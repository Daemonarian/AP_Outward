#!python3.13
"""
Setup the Python environment for this project.

This script is intended to be called using an appropriate python version either
from outside or inside the venv. If called from outside the venv, it will create
a default venv named '.venv'. It will then proceed to setup the venv by ensuring
that the appropriate packages are installed and search paths are registered.

On Windows, call with:
$> py setup.py

On Mac/Linux, call with:
$> ./setup.py
"""

import sys

python_version = (3, 13)
if (sys.version_info.major, sys.version_info.minor) != python_version:
    raise ValueError("requires Python {0}".format(".".join(python_version)))

from pathlib import Path
import subprocess

# defining relevant paths

archipelago_game_short_name = "outward"

project_dir = Path(__file__).parent
solution_dir = project_dir / ".."

apworld_codegen_project_dir = solution_dir / "SharedData"
apworld_codegen_project_setup_file = apworld_codegen_project_dir / "setup.py"
apworld_codegen_generate_file = apworld_codegen_project_dir / "src" / "generate_archipelago_files.py"

external_archipelago_project_dir = solution_dir / "External" / "Archipelago"
archipelago_module_update_file = external_archipelago_project_dir / "ModuleUpdate.py"
archipelago_game_world_dir = external_archipelago_project_dir / "worlds" / archipelago_game_short_name

source_game_world_dir = project_dir / "src" / "outward"

def get_venv_python(venv_dir):
    """
    Gets the path the venv's python appropriate for the current platform.
    """
    return venv_dir / "Scripts" / "python.exe" if sys.platform == "win32" else venv_dir / "bin" / "python"

venv_dir = project_dir / ".venv"
venv_python = get_venv_python(venv_dir)
my_path_configuration_file = venv_dir / "lib" / "site-packages" / "archipelago.pth"

apworld_codegen_venv_dir = apworld_codegen_project_dir / ".venv"
apworld_codegen_venv_python = get_venv_python(apworld_codegen_venv_dir)

subprocess.run([sys.executable, apworld_codegen_project_setup_file], check=True)

if not venv_dir.exists():
    subprocess.run([sys.executable, "-m", "venv", "--upgrade-deps", venv_dir], check=True)
subprocess.run([venv_python, "-m", "pip", "install", "--upgrade", "pip"], check=True)
subprocess.run([venv_python, archipelago_module_update_file, "--yes"], check=True)

search_paths = [external_archipelago_project_dir]
my_path_configuration_file_text = "\n".join(f"{p.resolve()}" for p in search_paths)
my_path_configuration_file.write_text(my_path_configuration_file_text)

subprocess.run([apworld_codegen_venv_python, apworld_codegen_generate_file])
