# SwissArmyKnife
![Banner](Images/Banner.png)

[![GitHub Actions Status](https://github.com/SCADAMINDS/swiss-army-knife/workflows/Build/badge.svg?branch=main)](https://github.com/SCADAMINDS/swiss-army-knife/actions)

[![GitHub Actions Build History](https://buildstats.info/github/chart/SCADAMINDS/swiss-army-knife?branch=main&includeBuildsFromPullRequest=false)](https://github.com/SCADAMINDS/swiss-army-knife/actions)


Project Description


This library is opinionated and expects you to have both Nodatime and JSON.NET installed.

# TODO
- Write readme


## Docs
- Write readme documentation
- Write Doxygen main page
- Write FluentTaskExtensions documentation


## Contribution
- Write introduction and running guide
  

## Code
- Add [Pure] annotations where necessary
- Add null checks where necessary. !! with c# 9 ?


## Other
- Publish to nuget on new releases
- block pushes to main


## Tests missing
- Test HttpResponseMessageExtensions
- Test ObjectExtensions
- Test FluentTaskExtensions


# CI
This project uses Github Actions to perform CI. It:
- Runs all tests and Roslyn Analyzers on the master branch, and on all PR's
- Runs code coverage via Codecov on all pull requests


# Contributing
You can pull or fork this project. It runs on dotnet core, so you should be able to run `dotnet test`
You cannot commit to `main` - but feel free to make a Pull Request.
Remember to add tests for your changes.

The CI pipelines use `cake` to run builds and tests, so if you only see errors in CI and not locally, try running the same commands.
First you will need to install the tool by running `dotnet tool restore`

Afterwards you can run build or tests via commands such as:
```shell
dotnet cake --target=Build
dotnet cake --target=Test
```

The project is built with `dotnet-boxed`, so if you have any questions to the structure, check out [this article](https://rehansaeed.com/the-fastest-nuget-package-ever-published-probably/)

Please run `dotnet format` before submitting your pull requests.

We use Doxygen to build the documentation until DocFx supports .NET core

# Release Process
Nuget releases happen automatically when a new Github release is made. The `release-drafter` github action should automatically generate release notes
if PR's have been labelled correctly.

All 

# Build documentation
Todo wait until DocFx supports .NET Core

For now, read inline documentation
