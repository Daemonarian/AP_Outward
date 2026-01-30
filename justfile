set shell := ["pwsh", "-Command"]
set dotenv-load := true

mod apworld "APWorld/justfile"
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
    @just apworld::setup

build:
    @just apworld::build
    @just mod::build

test:
    @just apworld::test
    @just mod::test

stage:
    @just apworld::stage
    @just mod::stage

release:
    @just apworld::release
    @just mod::release

install:
    @just apworld::install
    @just mod::install

deploy:
    @just apworld::deploy
    @just mod::install
