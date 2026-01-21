from enum import StrEnum
from pathlib import Path
from pydantic import AfterValidator, BaseModel, BeforeValidator, Field, PlainSerializer, computed_field, model_validator
import re
from typing import Annotated, Any, Dict, Iterable, List, Literal, Optional, Self, Set, TypeVar, Union
from pydantic_core import core_schema
from pydantic_core.core_schema import CoreSchema
import yaml

from common import data_file, to_csharp_int_array_literal, to_csharp_int_literal

def ensure_dict(data: Any, default_field: str = 'name') -> dict:
    if not isinstance(data, dict):
        return {default_field: data}
    return data

def dict_to_list(data: Any, key_field: str = 'key', default_field: str = 'name') -> Any:
    if isinstance(data, dict):
        return [{key_field: k, **ensure_dict(v, default_field=default_field)} for k, v in data.items()]
    return data

class Key:

    _snake_pattern = re.compile(r"[a-z][a-z0-9]*(_[a-z0-9]+)*")

    @classmethod
    def check_unique(cls, identifiers: Iterable[Self]) -> None:
        seen_identifiers: Dict[str, Set[str]] = dict()
        for identifier in identifiers:
            for k, v in identifier.all_cases.items():
                if k not in seen_identifiers:
                    seen_identifiers[k] = set()
                if v in seen_identifiers[k]:
                    raise ValueError(f"duplicate {k} case identifier: {v}")
                seen_identifiers[k].add(v)

    @classmethod
    def validate(cls, v: Any) -> Self:
        if isinstance(v, cls):
            return v
        if isinstance(v, str):
            return cls(v)
        raise ValueError(f"input must be a string or {cls.__name__}")

    @classmethod
    def serialize(cls, v: Self) -> str:
        return str(v)

    @classmethod
    def __get_pydantic_core_schema__(cls, source_type: Any, handler: Any) -> core_schema.CoreSchema:
        return core_schema.union_schema([
            core_schema.is_instance_schema(cls),
            core_schema.no_info_plain_validator_function(cls.validate),
        ])

    def __init__(self, snake: str):
        snake = str(snake)
        if self._snake_pattern.fullmatch(snake) is None:
            raise ValueError(f"`snake_case` should be a valid identifer name in snake_case, not '{snake}'")
        self._snake = snake
        self._upper = None
        self._pascal = None

    def __str__(self):
        return self.snake

    @property
    def snake(self) -> str:
        return self._snake

    @property
    def upper(self) -> str:
        if self._upper is None:
            self._upper = self.snake.upper()
        return self._upper

    @property
    def pascal(self) -> str:
        if self._pascal is None:
            self._pascal = ''.join(word.capitalize() for word in self.snake.split('_'))
        return self._pascal

    @property
    def all_cases(self) -> Dict[str, str]:
        return {
            'snake': self.snake,
            'upper': self.upper,
            'pascal': self.pascal
        }

class Event(BaseModel):
    key: Key
    short_name: Optional[str] = Field(default=None, alias='name')
    code: Optional[int] = None

    @computed_field
    @property
    def name(self) -> str:
        if self.short_name is not None:
            return f"Event - {self.short_name}"
        return f"Event - {self.key}"

class Location(BaseModel):
    key: Key
    name: str
    code: Optional[int] = None
    
class ItemClassification(StrEnum):
    filler = "filler"
    progression = "progression"
    useful = "useful"
    trap = "trap"
    progression_deprioritized_skip_balancing = "progression_deprioritized_skip_balancing"
    progression_skip_balancing = "progression_skip_balancing"
    progression_deprioritized = "progression_deprioritized"

class OutwardObjectType(StrEnum):
    money = "money"
    item = "item"
    skill = "skill"
    progressive_skill = "progressive_skill"

class OutwardMoney(BaseModel):
    object_type: Literal[OutwardObjectType.money] = Field(alias="type")
    amount: int

    def generate_giver(self) -> str:
        return f"new MoneyGiver({to_csharp_int_literal(self.amount)})"

class OutwardItem(BaseModel):
    object_type: Literal[OutwardObjectType.item] = Field(alias="type")
    item_id: int

    def generate_giver(self) -> str:
        return f"new ItemGiver({to_csharp_int_literal(self.item_id)})"

class OutwardSkill(BaseModel):
    object_type: Literal[OutwardObjectType.skill] = Field(alias="type")
    skill_id: int

    def generate_giver(self) -> str:
        return f"new SkillGiver({to_csharp_int_literal(self.skill_id)})"

class OutwardProgressiveSkill(BaseModel):
    object_type: Literal[OutwardObjectType.progressive_skill] = Field(alias="type")
    skill_ids: List[int]

    def generate_giver(self) -> str:
        return f"new ProgressiveSkillGiver({to_csharp_int_array_literal(self.skill_ids)})"

OutwardObject = Annotated[
    Union[
        OutwardMoney,
        OutwardItem,
        OutwardSkill,
        OutwardProgressiveSkill],
    Field(discriminator="object_type")]

class Item(BaseModel):
    key: Key
    name: str
    code: Optional[int] = None
    classification: ItemClassification
    count: Optional[int] = None
    outward: OutwardObject

    @model_validator(mode='after')
    def check_count(self) -> Self:
        if self.count is None:
            if isinstance(self.outward, OutwardProgressiveSkill):
                self.count = len(self.outward.skill_ids)
            else:
                self.count = 1
        return self

T = TypeVar('T', bound=BaseModel)
KeyedList = Annotated[List[T], BeforeValidator(dict_to_list)]

class DataSchema(BaseModel):
    events: KeyedList[Event]
    locations: KeyedList[Location]
    items: KeyedList[Item]

    @model_validator(mode='after')
    def check_unique(self) -> Self:
        Key.check_unique(event.key for event in self.events)
        Key.check_unique(location.key for location in self.locations)
        Key.check_unique(item.key for item in self.items)

        location_codes: Set[int] = set()
        item_codes: Set[int] = set()
        for event in self.events:
            if event.code is not None:
                if event.code in location_codes:
                    raise ValueError(f"duplicate location code: {event.code}")
                if event.code in item_codes:
                    raise ValueError(f"duplicate item code: {event.code}")
                location_codes.add(event.code)
                item_codes.add(event.code)
        for location in self.locations:
            if location.code is not None:
                if location.code in location_codes:
                    raise ValueError(f"duplicate location code: {location.code}")
                location_codes.add(location.code)
        for item in self.items:
            if item.code is not None:
                if item.code in item_codes:
                    raise ValueError(f"duplicate item code: {item.code}")
                item_codes.add(item.code)

        location_code = 1
        for location in sorted(self.locations, key=lambda x: x.key.snake):
            if location.code is None:
                while location_code in location_codes:
                    location_code += 1
                location.code = location_code
                location_codes.add(location.code)

        item_code = 1
        for item in sorted(self.items, key=lambda x: x.key.snake):
            if item.code is None:
                while item_code in item_codes:
                    item_code += 1
                item.code = item_code
                item_codes.add(item.code)

        event_code = max(location_codes | item_codes, default=0) + 1
        for event in sorted(self.events, key=lambda x: x.key.snake):
            if event.code is None:
                while event_code in location_codes or event_code in item_codes:
                    event_code += 1
                event.code = event_code
                location_codes.add(event.code)
                item_codes.add(event.code)

        return self

def load_data(path: Optional[Path] = None) -> DataSchema:
    if path is None:
        path = data_file
    with path.open('r', encoding='utf-8') as f:
        raw_data = yaml.safe_load(f)
    return DataSchema.model_validate(raw_data)

if __name__ == '__main__':
    data = load_data()
    json = data.model_dump_json(indent=2)
    print(json)
