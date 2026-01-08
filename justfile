set shell := ["pwsh", "-Command"]
set dotenv-load := true

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
    @just archipelago::clean
    @just outward::clean

setup:
    @just archipelago::setup

build:
    @just archipelago::build
    @just outward::build

test:
    @just archipelago::test
    @just outward::test

stage:
    @just archipelago::stage
    @just outward::stage

release:
    @just archipelago::release
    @just outward::release

install:
    @just archipelago::install
    @just outward::install

deploy:
    @just archipelago::deploy
    @just outward::install
    @just outward::watch-log
