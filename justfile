set shell := ["pwsh", "-Command"]
set dotenv-load := true

mod shared "SharedData/justfile"
mod archipelago "Archipelago/justfile"
mod outward "Outward/justfile"

build_dir := "build"
release_dir := "release"

default:
    @just --list

nuke:
    git clean -fdx -e ".env"

clean:
    if (Test-Path -Path "{{build_dir}}" -PathType Container) { Remove-Item -Path "{{release_dir}}" -Recurse -Force }
    if (Test-Path -Path "{{build_dir}}" -PathType Container) { Remove-Item -Path "{{build_dir}}" -Recurse -Force }
    @just outward::clean
    @just archipelago::clean
    @just shared::clean

setup:
    @just shared::build
    @just archipelago::setup

build:
    @just shared::build
    @just archipelago::build
    @just outward::build

test:
    @just shared::test
    @just archipelago::test
    @just outward::test

stage:
    @just shared::build
    @just archipelago::stage
    @just outward::stage

release:
    @just shared::build
    @just archipelago::release
    @just outward::release

install:
    @just shared::build
    @just archipelago::install
    @just outward::install

deploy:
    @just shared::build
    @just archipelago::deploy
    @just outward::install
