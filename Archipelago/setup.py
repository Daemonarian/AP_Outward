#!/usr/bin/env python3.12

r"""
This is a custom setup script for the AP_Outward APWorld subproject.
It is intended to imitate the interface of setuptools' setup.py scripts,
but instead of installing a PyPI package, it sets up a development environment
for the AP_Outward subproject within the larger Archipelago project, and
allows for easy builds.
"""

import argparse
import shutil
import subprocess
import sys
import urllib.request

from enum import IntEnum
from pathlib import Path
from typing import Iterator, List, Optional

# load the project information

here = Path(__file__).parent.resolve()
version_file = here / ".." / "VERSION"
archipelago_version_file = here / ".." / "ARCHIPELAGO_VERSION"
build_dir = here / "build"
archipelago_archive_file = build_dir / "Archipelago.tgz"
archipelago_archive_temp_dir = build_dir / "Archipelago_temp"
archipelago_source_dir = here / "Archipelago"
archipelago_source_file = build_dir / "Archipelago_sentinel"
archipelago_setup_file = archipelago_source_dir / "setup.py"
archipelago_apworld_dir = archipelago_source_dir / "worlds" / "outward"
source_dir = here / "src"
source_apworld_dir = source_dir / "outward"
release_dir = here / "release"
release_apworld_file = release_dir / "outward.apworld"

venv_dir = archipelago_source_dir / ".venv"
venv_python = venv_dir / "Scripts" / "python.exe" if sys.platform == "win32" else venv_dir / "bin" / "python"

archipelago_version = archipelago_version_file.read_text().strip()

class PackageInfo:
    name: str = "outward_apworld"
    version: str = version_file.read_text().strip()
    author: str = "Daemonarian"
    author_email: str = "daemonarian@gmail.com"
    maintainer: Optional[str] = "Daemonarian"
    maintainer_email: Optional[str] = "daemonarian@gmail.com"
    url: str = "https://github.com/Daemonarian/AP_Outward/tree/main/Archipelago"
    license: Optional[str] = None
    description: str = "Archipelago APWorld for the game Outward: Definitive Edition"
    long_description: str = (here / "README.md").read_text()
    platforms: List[str] = ["any"]
    classifiers: List[str] = [
        "Development Status :: 2 - Pre-Alpha",
        "Environment :: Plugins",
        "Intended Audience :: End Users/Desktop",
        "Operating System :: OS Independent",
        "Programming Language :: Python :: 3.12",
        "Topic :: Games/Entertainment",
    ]
    keywords: List[str] = ["archipelago", "multiplayer", "randomizer", "outward", "apworld"]
    provides: List[str] = []
    requires: List[str] = []
    obsoletes: List[str] = []

# a basic logging mechanism supporting verbosity levels

class LogLevel(IntEnum):
    ERROR = 0
    QUIET = 1
    VERBOSE = 2

log_level: LogLevel = LogLevel.VERBOSE

def log(message: str, level: LogLevel = LogLevel.VERBOSE) -> None:
    if log_level >= level:
        print(str(message))

# a global flag for dry-run mode
do_dry_run: bool = False
do_ignore_user_config: bool = False
command_packages: Optional[str] = None

# some utility functions

def expand_dirs(paths: List[Path]) -> Iterator[Path]:
    r"""
    Expands any directories in the given list of paths into their constituent files.
    Returns a new list of paths with directories replaced by their files.
    """

    for path in paths:
        path = Path(path)
        if path.exists() and path.is_dir():
            for (dirpath, _, filenames) in path.walk():
                for filename in filenames:
                    yield dirpath / filename
        else:
            yield path

def force_remove_dir(path: Path) -> None:
    r"""
    Recursively removes the given directory and all its contents.
    """

    if path.exists():
        for (dirpath, dirnames, filenames) in path.walk(top_down=False):
            for filename in filenames:
                (dirpath / filename).unlink()
            for dirname in dirnames:
                (dirpath / dirname).rmdir()
        path.rmdir()

def test_dependency(targets: Path, dependencies: List[Path]) -> bool:
    r"""
    Tests whether any of the target files are out of date with respect to any of the dependdncy files.
    If any dependency file is newer than any target file, or if any target file does not exist,
    returns True. Otherwise, returns False.
    """

    target_mtime: Optional[int] = None
    for target in expand_dirs(targets):
        if not target.exists():
            return True
        file_mtime = target.stat().st_mtime_ns
        if target_mtime is None or file_mtime < target_mtime:
            target_mtime = file_mtime
     
    if target_mtime is None:
        return True

    for dependency in expand_dirs(dependencies):
        if not dependency.exists():
            return True
        file_mtime = dependency.stat().st_mtime_ns
        if file_mtime > target_mtime:
            return True

    return False

def create_directory_link(link: Path, target: Path) -> None:
    r"""
    Creates a symbolic link at the target path pointing to the source path.
    """
    if link.exists():
        raise FileExistsError(f"Target path {link} already exists")
    if sys.platform == "win32":
        subprocess.run(["cmd", "/c", "mklink", "/J", str(link), str(target)], check=True)
    else:
        link.symlink_to(target, target_is_directory=True)

def run_sub_setup(args: List[str]) -> None:
    r"""
    Runs the Archipelago setup.py script with the given arguments.
    """
    
    subprocess.run([sys.executable, "-m", "venv", "--upgrade-deps", str(venv_dir)], check=True)

    full_args: List[str] = [venv_python, str(archipelago_setup_file), "--yes"]
    if log_level == LogLevel.QUIET:
        full_args.append("--quiet")
    if do_dry_run:
        full_args.append("--dry-run")
    if do_ignore_user_config:
        full_args.append("--no-user-cfg")
    if command_packages is not None:
        full_args.extend(["--command-packages", command_packages])
    full_args.extend(args)
    
    subprocess.run(full_args, cwd=archipelago_source_dir, check=True)
    
def clean_dir(path: Path) -> None:
    if path.exists():
        log(f"Deleting '{path}'...")
        if not do_dry_run:
            force_remove_dir(path)

# command implementations

def setup_env():
    r"""
    Sets up the AP_Outward APWorld development environment.
    """

    log("Setting up AP_Outward APWorld development environment...")

    if test_dependency([archipelago_archive_file], [archipelago_version_file]):
        log(f"Downloading Archipelago version {archipelago_version} source code...")
        if not do_dry_run:
            build_dir.mkdir(parents=True, exist_ok=True)
            try:
                urllib.request.urlretrieve(f"https://github.com/ArchipelagoMW/Archipelago/archive/refs/tags/{archipelago_version}.tar.gz", archipelago_archive_file)
            except:
                archipelago_archive_file.unlink(missing_ok=True)
                raise

    if test_dependency([archipelago_source_file], [archipelago_archive_file, archipelago_version_file]):
        log("Extracting Archipelago source code...")
        if not do_dry_run:
            force_remove_dir(archipelago_archive_temp_dir)

            try:
                archipelago_archive_temp_dir.mkdir(parents=True, exist_ok=True)
                shutil.unpack_archive(archipelago_archive_file, archipelago_archive_temp_dir)

                archipelago_archive_contents = list(archipelago_archive_temp_dir.iterdir())
                if len(archipelago_archive_contents) != 1 or not archipelago_archive_contents[0].is_dir():
                    raise RuntimeError("Unexpected contents in Archipelago archive")

                extracted_archipelago_dir = archipelago_archive_contents[0]
                archipelago_source_file.unlink(missing_ok=True)
                archipelago_apworld_dir.unlink(missing_ok=True)
                force_remove_dir(archipelago_source_dir)
                try:
                    shutil.move(extracted_archipelago_dir, archipelago_source_dir)
                    create_directory_link(archipelago_apworld_dir, source_apworld_dir)
                    archipelago_source_file.touch()
                except:
                    archipelago_apworld_dir.unlink(missing_ok=True)
                    force_remove_dir(archipelago_source_dir)
                    raise
            finally:
                force_remove_dir(archipelago_archive_temp_dir)

    log("Setting up python virtual environment...")
    if not do_dry_run:
        subprocess.run([sys.executable, "-m", "venv", "--upgrade-deps", str(venv_dir)], check=True)
    for (dirpath, _, filenames) in archipelago_source_dir.walk(top_down=True):
        for filename in filenames:
            if filename == "requirements.txt" or filename == "ci_requirements.txt":
                requirements_file = dirpath / filename
                log(f"Installing dependencies from '{requirements_file}'...")
                if not do_dry_run:
                    subprocess.run([venv_python, "-m", "pip", "install", "-r", str(requirements_file)], check=True)

def build():
    setup_env()
    run_sub_setup(["build", "--yes"])

    log("Copying built files to release directory...")
    if not do_dry_run:
        force_remove_dir(release_dir)

        archipelago_apworld_file: Optional[Path] = None
        for (dirpath, dirnames, filenames) in (archipelago_source_dir / "build").walk():
            for filename in filenames:
                if filename == "outward.apworld":
                    if archipelago_apworld_file is not None:
                        raise RuntimeError("Multiple 'outward.apworld' files found in build output")
                    archipelago_apworld_file = dirpath / filename
        if archipelago_apworld_file is None:
            raise RuntimeError("No 'outward.apworld' file found in build output")
        
        release_apworld_file.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy(archipelago_apworld_dir, release_apworld_file)

def clean():
    log("Cleaning build artifacts...")
    clean_dir(release_dir)
    if archipelago_setup_file.exists():
        run_sub_setup(["clean"])

def clean_env():
    log("Cleaning development environment...")
    clean_dir(release_dir)
    archipelago_apworld_dir.unlink(missing_ok=True)
    clean_dir(archipelago_source_dir)
    clean_dir(build_dir)
    clean_dir(venv_dir)

def test():
    setup_env()

    log("Installing test dependencies...")
    if not do_dry_run:
        requirements_files: List[Path] = []
        for (dirpath, _, filenames) in archipelago_source_dir.walk():
            for filename in filenames:
                if filename == "requirements.txt" or filename == "ci_requirements.txt":
                    requirements_files.append(dirpath / filename)

        pip_args = [venv_python, "-m", "pip", "install", "--upgrade"]
        for requirements_file in requirements_files:
            pip_args.extend(["-r", str(requirements_file)])
        subprocess.run(pip_args, check=False)

    log("Running tests...")
    if not do_dry_run:
        subprocess.run([venv_python, "-m", "pytest"], cwd=str(archipelago_source_dir), check=True)

# parse args

parser = argparse.ArgumentParser(description="Custom setup script for AP_Outward APWorld subproject.")

global_options = parser.add_argument_group("Global", "options that apply to all commands")
global_options.add_argument("--verbose", "-v", action="store_true", help="run verbosely (default)")
global_options.add_argument("--quiet", "-q", action="store_true", help="run quietly (turns verbosity off)")
global_options.add_argument("--dry-run", "-n", action="store_true", help="don't actually do anything")
global_options.add_argument("--no-user-cfg", action="store_true", help="ignore pydistutils.cfg in your home directory")
global_options.add_argument("--command-packages", type=str, help="list of packages that provide distutils commands")

information_options = parser.add_argument_group("Information", "just display information, ignore any commands")
information_options.add_argument("--help-commands", action="store_true", help="list all available commands")
information_options.add_argument("--name", action="store_true", help="print package name")
information_options.add_argument("--version", "-V", action="store_true", help="print package version")
information_options.add_argument("--fullname", action="store_true", help="print <package name>-<version>")
information_options.add_argument("--author", action="store_true", help="print the author's name")
information_options.add_argument("--author-email", action="store_true", help="print the author's email address")
information_options.add_argument("--maintainer", action="store_true", help="print the maintainer's name")
information_options.add_argument("--maintainer-email", action="store_true", help="print the maintainer's email address")
information_options.add_argument("--contact", action="store_true", help="print the maintainer's name if known, else the author's")
information_options.add_argument("--contact-email", action="store_true", help="print the maintainer's email address if known, else the author's")
information_options.add_argument("--url", action="store_true", help="print the URL for this package")
information_options.add_argument("--license", "--licence", action="store_true", help="print the license of the package")
information_options.add_argument("--description", action="store_true", help="print the package description")
information_options.add_argument("--long-description", action="store_true", help="print the long package description")
information_options.add_argument("--platforms", action="store_true", help="print the list of platforms")
information_options.add_argument("--classifiers", action="store_true", help="print the list of classifiers")
information_options.add_argument("--keywords", action="store_true", help="print the list of keywords")
information_options.add_argument("--provides", action="store_true", help="print the list of packages/modules provided")
information_options.add_argument("--requires", action="store_true", help="print the list of packages/modules required")
information_options.add_argument("--obsoletes", action="store_true", help="print the list of packages/modules made obsolete")

subparsers = parser.add_subparsers(dest="command", description="Available commands")
subparsers.add_parser("setup_env", help="setup the AP_Outward development environment")
subparsers.add_parser("build", help="build the AP_Outward APWorld package")
subparsers.add_parser("clean", help="clean build artifacts")
subparsers.add_parser("clean_env", help="clean the AP_Outward development environment")
subparsers.add_parser("test", help="run the test suite")

args = parser.parse_args()

if args.verbose:
    log_level = LogLevel.VERBOSE
if args.quiet:
    log_level = LogLevel.QUIET
if args.dry_run:
    do_dry_run = True
if args.no_user_cfg:
    do_ignore_user_config = True
if args.command_packages:
    command_packages = args.command_packages

did_print_info: bool = False
if args.help_commands:
    did_print_info = True
    print("Available commands:")
    print("  setup_env       setup the AP_Outward development environment")
    print("  build           build the AP_Outward APWorld package")
    print("  clean           clean build artifacts")
    print("  clean_env       clean the AP_Outward development environment")
    print("  test            run the test suite")
if args.name:
    did_print_info = True
    print(PackageInfo.name)
if args.version:
    did_print_info = True
    print(PackageInfo.version)
if args.fullname:
    did_print_info = True
    print(f"{PackageInfo.name}-{PackageInfo.version}")
if args.author:
    did_print_info = True
    print(PackageInfo.author)
if args.author_email:
    did_print_info = True
    print(PackageInfo.author_email)
if args.maintainer:
    did_print_info = True
    print(PackageInfo.maintainer)
if args.maintainer_email:
    did_print_info = True
    print(PackageInfo.maintainer_email)
if args.contact:
    did_print_info = True
    contact_name = PackageInfo.maintainer if PackageInfo.maintainer else PackageInfo.author
    print(contact_name)
if args.contact_email:
    did_print_info = True
    contact_email = PackageInfo.maintainer_email if PackageInfo.maintainer_email else PackageInfo.author_email
    print(contact_email)
if args.url:
    did_print_info = True
    print(PackageInfo.url)
if args.license:
    did_print_info = True
    print(PackageInfo.license)
if args.description:
    did_print_info = True
    print(PackageInfo.description)
if args.long_description:
    did_print_info = True
    print(PackageInfo.long_description)
if args.platforms:
    did_print_info = True
    print(", ".join(PackageInfo.platforms))
if args.classifiers:
    did_print_info = True
    print("\n".join(PackageInfo.classifiers))
if args.keywords:
    did_print_info = True
    print(", ".join(PackageInfo.keywords))
if args.provides:
    did_print_info = True
    print(", ".join(PackageInfo.provides))
if args.requires:
    did_print_info = True
    print(", ".join(PackageInfo.requires))
if args.obsoletes:
    did_print_info = True
    print(", ".join(PackageInfo.obsoletes))
if did_print_info:
    exit(0)

if args.command:
    if args.command == "setup_env":
        setup_env()
    elif args.command == "build":
        build()
    elif args.command == "clean":
        clean()
    elif args.command == "clean_env":
        clean_env()
    elif args.command == "test":
        test()
    else:
        log(f"Unknown command: {args.command}", level=LogLevel.ERROR)
        exit(1)
