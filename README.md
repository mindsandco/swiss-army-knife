# SwissArmyKnife
![Banner](Images/scm_banner.png)

[![GitHub Actions Status](https://github.com/SCADAMINDS/swiss-army-knife/workflows/Build/badge.svg?branch=main)](https://github.com/SCADAMINDS/swiss-army-knife/actions)

[![GitHub Actions Build History](https://buildstats.info/github/chart/SCADAMINDS/swiss-army-knife?branch=main&includeBuildsFromPullRequest=false)](https://github.com/SCADAMINDS/swiss-army-knife/actions)

This is a project full of utility methods that can be used across multiple projects.  
The code is:
- Thoroughly documented
- Thoroughly tested
- Methods that are side-effect free are marked as `[Pure]`

# Installation
This library can be installed from Nuget or Github Packages.

# TODO nuget
# TODO github package repository


## SwissArmyKnife.Extensions
A bunch of handy extension methods that you'll probably like to use!
Examples:
```csharp

// ------------- Dictionary.GetOr
myDictionary.GetOr("nonExistingKey", () => "myFallbackValue");


// ------------- object.Yield
// Produce an Enumerable out of an item
int myItem = 3;

// Type: IEnumerable<int>
var myEnumerable = myItem.Yield();

// ------------- HttpClient.GetAsJsonAsync()
var client = new HttpClient();

var url = "http://www.some-url-that-produces-json.com"

// Gets URL and serializes model to MyResponseModel. On error prints http response
var await response = client.GetAsJsonAsync<MyResponseModel>(url)

```

[And many more!](https://scadaminds.github.io/swiss-army-knife/html/md_Documentation_Extensions.html)

## Documentation
We automatically build the documentation based on some written docs and the source code.
You can find the main page of the generated documentation [here](https://scadaminds.github.io/swiss-army-knife/html/index.html)


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


### Before submitting a PR

- Please run `dotnet format` before submitting your pull requests.
- We use Doxygen to build the documentation until DocFx supports .NET core
  Please install it and run `doxygen doxygen-config.toml` to generate up-to-date documentation if needed.



# Release Process
Nuget releases happen automatically when a new Github release is made. The `release-drafter` github action should automatically generate release notes if PR's have been labelled correctly.

Each merge into the `main` branch triggers a release of a Github Packages package.


# TODO list

## Docs
- Write readme documentation
- Write Doxygen main page
- Write FluentTaskExtensions documentation


## Contribution
- Write introduction and running guide

## Other
- Publish to nuget on new releases
- block pushes to main


## Tests missing
- Test HttpResponseMessageExtensions
- Test ObjectExtensions
- Test FluentTaskExtensions
