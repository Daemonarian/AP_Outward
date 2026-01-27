"""
A module with some common code for the exporters.
"""

from collections.abc import Iterator
from contextlib import contextmanager
import sys
from typing import TextIO

@contextmanager
def _open(file: str) -> Iterator[TextIO]:
    fp = sys.stdout if file == "-" else open(file, "w")
    try:
        yield fp
    finally:
        if fp is not None and fp is not sys.stdout:
            fp.close()
        fp = None
