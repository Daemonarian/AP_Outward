"""
This module contains utilities for defining properties of various Outward objects all in one place.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from .common import BASE_ID

if TYPE_CHECKING:
    from collections.abc import Callable, Iterable
    from typing import Self

class OutwardObjectTemplate:
    def __init_subclass__(cls):
        cls._name_to_template: dict[str, Self] = dict()

    @classmethod
    def register_template[**P](cls: Callable[P, Self], *args: P.args, **kwargs: P.kwargs) -> str:
        template = cls(*args, **kwargs)
        name = template.name
        if name in cls._name_to_template:
            raise ValueError(f"cannot name two instance of {cls.__name__} the same name: '{name}'")
        cls._name_to_template[name] = template
        return template.name

    @classmethod
    def get_template(cls, name: str) -> Self:
        template = cls._name_to_template[name]
        if not isinstance(template, cls):
            raise ValueError(f"the template with that name was not of the expected type: '{name}'")
        return template

    @classmethod
    def get_templates(cls) -> Iterable[Self]:
        return cls._name_to_template.values()

    @classmethod
    def get_names(cls) -> Iterable[str]:
        return cls._name_to_template.keys()

    _name: str

    def __init__(self, name: str):
        self._name = name

    @property
    def name(self) -> str:
        return self._name

class OutwardGameObjectTemplate(OutwardObjectTemplate):
    @classmethod
    def get_name_to_id(cls) -> dict[str, int]:
        """
        Generate IDs for the items/locations/events.

        When generating IDs, we ensure that the IDs are ordered by the same order as
        the given namespaces. We also respect any IDs that were manually set before-hand.

        :param namespace: The class acting as namespaces for all the name constants.
        :param prop_cls: The type of the Properties object.

        :return: A dict mapping names to property objects of type prop_cls.
        """

        # sort the objects by name to ensure consistency in the generated ids
        templates = list(cls.get_templates())
        templates.sort(key=lambda template: template.name)
    
        # generate and set the property ids on the property objects
        curr_id = BASE_ID
        used_ids = set(template.archipelago_id for template in templates)
        for template in templates:
            if template.archipelago_id < 0:
                while curr_id in used_ids:
                    curr_id += 1
                template.archipelago_id = curr_id
                curr_id += 1

        return {template.name: template.archipelago_id for template in templates}

    _archipelago_id: int
    
    def __init__(self, name: str, archipelago_id: int = -1):
        super().__init__(name)
        self._archipelago_id = archipelago_id

    @property
    def archipelago_id(self) -> int:
        return self._archipelago_id

    @archipelago_id.setter
    def archipelago_id(self, archipelago_id: int) -> None:
        if archipelago_id < 0:
            raise ValueError("'archipelago_id' should be non-negative")
        if self._archipelago_id >= 0:
            raise ValueError("cannot change an already set 'archipelago_id'")
        self._archipelago_id = archipelago_id

class OutwardObjectNamespace:
    template: type[OutwardObjectTemplate] | None = None

    @classmethod
    def get_names(cls) -> Iterable[str]:
        if cls.template is None:
            raise ValueError(f"cannot get names for a namespace with no template")
        return cls.template.get_names()

class OutwardGameObjectNamespace(OutwardObjectNamespace):
    template: type[OutwardGameObjectTemplate] | None

    @classmethod
    def get_name_to_id(cls) -> dict[str, int]:
        if cls.template is None:
            raise ValueError(f"cannot get name-to-id mapping for a namespace with no template")
        return cls.template.get_name_to_id()
