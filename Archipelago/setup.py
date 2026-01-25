#!python3.13

import sys

python_version = (3, 13)
if (sys.version_info.major, sys.version_info.minor) != python_version:
    raise ValueError("requires Python {0}".format(".".join(python_version)))

from pathlib import Path
import subprocess

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
    return venv_dir / "Scripts" / "python.exe" if sys.platform == "win32" else venv_dir / "bin" / "python"

venv_dir = project_dir / ".venv"
venv_python = get_venv_python(venv_dir)

apworld_codegen_venv_dir = apworld_codegen_project_dir / ".venv"
apworld_codegen_venv_python = get_venv_python(apworld_codegen_venv_dir)

def make_symlink(link_name, link_target):
    link_name = Path(link_name)
    link_target = Path(link_target)
    if sys.platform == "win32":
        subprocess.run(["cmd", "/c", "mklink", "/J", link_name, link_target], stdin=subprocess.DEVNULL, check=True)
    else:
        link_name.symlink_to(link_target, target_is_directory=True)

subprocess.run([sys.executable, apworld_codegen_project_setup_file], check=True)

if not venv_dir.exists():
    subprocess.run([sys.executable, "-m", "venv", "--upgrade-deps", venv_dir], check=True)
subprocess.run([venv_python, "-m", "pip", "install", "--upgrade", "pip"], check=True)
subprocess.run([venv_python, archipelago_module_update_file, "--yes"], check=True)

if not archipelago_game_world_dir.exists():
    make_symlink(archipelago_game_world_dir, source_game_world_dir)

subprocess.run([apworld_codegen_venv_python, apworld_codegen_generate_file])
