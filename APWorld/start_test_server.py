"""
This script generates and launches a multi-world with just a single player
suitable for manual testing.
"""

from pathlib import Path
import os
import shutil
import subprocess
import sys

host = 'localhost'
port = 38281
password = 'password'

project_dir = Path(__file__).parent
venv_dir = project_dir / '.venv'
venv_python = venv_dir / 'Scripts' / 'python.exe' if sys.platform == 'win32' else venv_dir / 'bin' / 'python'

test_dir = project_dir / 'bin' / 'test_env'
test_players_dir = test_dir / 'players'
test_output_dir = test_dir / 'output'
multiworld_file = test_dir / 'test.archipelago'

external_archipelago_dir = project_dir / '..' / 'External' / 'Archipelago'
default_options_file = external_archipelago_dir / 'Players' / 'Templates' / 'Outward Definitive Edition.yaml'

# ensure we are running in the venv
if not Path(sys.executable).samefile(venv_python):
    proc = subprocess.run([venv_python, *sys.argv])
    sys.exit(proc.returncode)

# if in Windows, relaunch in a new console window
if sys.platform == 'win32':
    is_new_window_key = '_START_TEST_SERVER_NEW_WINDOW'
    is_new_window = os.getenv(is_new_window_key) is not None
    if not is_new_window:
        my_env = os.environ.copy()
        my_env[is_new_window_key] = '1'
        subprocess.Popen([sys.executable, *sys.argv], env=my_env, creationflags=subprocess.CREATE_NEW_CONSOLE)
        sys.exit(0)

# cleanup old test dir
if test_dir.exists():
    shutil.rmtree(test_dir)

# generate default yaml
subprocess.run([sys.executable, '-m', 'Launcher', 'Generate Template Options', '--', '--skip_open_folder'], check=True)

# copy default options to players dir
if test_players_dir.exists():
    shutil.rmtree(test_players_dir)
test_players_dir.mkdir(parents=True)
shutil.copy(default_options_file, test_players_dir / 'Player1.yaml')

# generate multi-world
if test_output_dir.exists():
    shutil.rmtree(test_output_dir)
test_output_dir.mkdir(parents=True)
subprocess.run([sys.executable, '-m', 'Launcher', 'Generate', '--', '--player_files_path', str(test_players_dir), '--outputpath', str(test_output_dir)])

# move multiworld file to the root of the test directory
archive = None
for child in test_output_dir.iterdir():
    if child.is_file() and child.suffix == '.zip' and child.stem.startswith('AP_'):
        if archive is None:
            archive = child
        else:
            raise Exception("multiple generated archives were found")
if archive is None:
    raise Exception("could not find the generated archive")

archive_contents_dir = test_output_dir / archive.stem
if archive_contents_dir.exists():
    shutil.rmtree(archive_contents_dir)
archive_contents_dir.mkdir(parents=True)
shutil.unpack_archive(archive, archive_contents_dir)

multidata = None
for child in archive_contents_dir.iterdir():
    if child.is_file() and child.suffix == '.archipelago' and child.stem == archive.stem:
        if multidata is None:
            multidata = child
        else:
            raise Exception("multiple possible multidata files were found")
if multidata is None:
    raise Exception("could not find multidata file")

multiworld_file.parent.mkdir(parents=True, exist_ok=True)
shutil.copy(multidata, multiworld_file)

# launch the multi-server
subprocess.run([sys.executable, '-m', 'Launcher', 'MultiServer', '--', '--host', str(host), '--port', str(port), '--password', str(password), str(multiworld_file.resolve())], cwd=str(test_dir.resolve()), check=True)
