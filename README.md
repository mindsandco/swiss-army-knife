# SwissArmyKnife
![Banner](Images/Banner.png)

[![GitHub Actions Status](https://github.com/SCADAMINDS/swiss-army-knife/workflows/Build/badge.svg?branch=main)](https://github.com/SCADAMINDS/swiss-army-knife/actions)

[![GitHub Actions Build History](https://buildstats.info/github/chart/SCADAMINDS/swiss-army-knife?branch=main&includeBuildsFromPullRequest=false)](https://github.com/SCADAMINDS/swiss-army-knife/actions)


Project Description


This library is opinionated and expects you to have both Nodatime and JSON.NET installed.

# TODO

## Docs
- Write readme documentation
- Build docs using doxygen or similar
  

## Other
- change key.snk ?


## Contribution
- Write introduction and running guide
  

## Code
- Fix billion warnings in StyleCop. Build docs first and see what you actually need
- Add [Pure] annotations where necessary
- Add null checks where necessary. !! with c# 9 ?


## Other
- Setup CI & Github
- Generate documentation
- Publish to nuget on new releases


## Tests missing
- Test HttpResponseMessageExtensions
- Test ObjectExtensions
- Test TaskExtensions


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

# Release Process
Nuget releases happen automatically when a new Github release is made. The `release-drafter` github action should automatically generate release notes
if PR's have been labelled correctly.

All 

# Build documentation
Todo wait until DocFx supports .NET Core

For now, read inline documentation
