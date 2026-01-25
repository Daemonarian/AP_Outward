#!python3.13

import sys

python_version = (3, 13)
if (sys.version_info.major, sys.version_info.minor) != python_version:
    raise ValueError("requires Python {0}".format(".".join(python_version)))

from pathlib import Path
import subprocess

here = Path(__file__).parent

venv_dir = here / ".venv"
venv_python = venv_dir / "Scripts" / "python.exe" if sys.platform == "win32" else venv_dir / "bin" / "python"

requirements_file = here / "requirements.txt"

if not venv_dir.exists():
    subprocess.run([sys.executable, "-m", "venv", "--upgrade-deps", venv_dir], check=True)
subprocess.run([venv_python, "-m", "pip", "install", "--upgrade", "pip"], check=True)
subprocess.run([venv_python, "-m", "pip", "install", "-r", requirements_file], check=True)
