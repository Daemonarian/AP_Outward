"""
Common utility functions for the build tools.
"""

from __future__ import annotations
from typing import TYPE_CHECKING

from contextlib import contextmanager
import sys

if TYPE_CHECKING:
    from collections.abc import Iterator
    from typing import TextIO

@contextmanager
def open_special(file: str, mode: str = 'w') -> Iterator[TextIO]:
    if file == '-':
        if 'w' in mode:
            fp = sys.stdout
        else:
            fp = sys.stdin
    else:
        fp = open(file, mode)
    try:
        yield fp
    finally:
        if fp is not None and fp is not sys.stdin and fp is not sys.stdout and fp is not sys.stderr:
            fp.close()
        fp = None
