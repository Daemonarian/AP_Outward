set shell := ["pwsh", "-Command"]
set dotenv-load := true

mod codegen "SharedData/justfile"
mod apworld "Archipelago/justfile"
mod mod "Outward/justfile"

build_dir := "build"
release_dir := "release"

default:
    @just --list

nuke:
    git clean -fdx -e ".env"

clean:
    if (Test-Path -Path "{{build_dir}}" -PathType Container) { Remove-Item -Path "{{release_dir}}" -Recurse -Force }
    if (Test-Path -Path "{{build_dir}}" -PathType Container) { Remove-Item -Path "{{build_dir}}" -Recurse -Force }
    @just mod::clean
    @just apworld::clean
    @just codegen::clean

setup:
    @just codegen::build
    @just apworld::setup

build:
    @just codegen::build
    @just apworld::build
    @just mod::build

test:
    @just codegen::test
    @just apworld::test
    @just mod::test

stage:
    @just codegen::build
    @just apworld::stage
    @just mod::stage

release:
    @just codegen::build
    @just apworld::release
    @just mod::release

install:
    @just codegen::build
    @just apworld::install
    @just mod::install

deploy:
    @just codegen::build
    @just apworld::deploy
    @just mod::install
