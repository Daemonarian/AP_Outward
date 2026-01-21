import re
from pathlib import Path
from typing import List

here = Path(__file__).parent

data_file = here / '..' / 'data' / 'data.yaml'

archipelago_gen_dir = here / '..' / '..' / 'Archipelago' / 'src' / 'outward'
archipelago_event_file = archipelago_gen_dir / 'event_data.py'
archipelago_location_file = archipelago_gen_dir / 'location_data.py'
archipelago_item_file = archipelago_gen_dir / 'item_data.py'

outward_build_dir = here / '..' / '..' / 'Outward' / 'src' / 'Archipelago' / 'Data'
outward_location_file = outward_build_dir / 'ArchipelagoLocationData.cs'
outward_item_file = outward_build_dir / 'ArchipelagoItemData.cs'

def to_csharp_int_literal(v: int) -> str:
    return repr(v)

def to_csharp_int_array_literal(v: List[int]) -> str:
    return f"new int[] {{ {', '.join(to_csharp_int_literal(x) for x in v)} }}"

def to_csharp_string_literal(s: str) -> str:
    s = s.replace("\\", "\\\\")
    s = s.replace("\"", "\\\"")
    s = s.replace("\n", "\\n")
    s = s.replace("\r", "\\r")
    s = s.replace("\t", "\\t")
    s = s.replace("\0", "\\0")
    s = s.replace("\b", "\\b")
    s = s.replace("\f", "\\f")
    return f"\"{s}\""

_trailing_whitespace_pattern = re.compile(r"[ \t\r\f\v]+$", re.MULTILINE)
def write_code_file(path: Path, data: str, encoding: str = 'utf-8', newline: str = '\n') -> None:
    path = Path(path)

    data = str(data).strip() + '\n'
    data = _trailing_whitespace_pattern.sub("", data)

    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(data, encoding=encoding, newline=newline)
