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

# Building the documentation locally
To build the documentation locally to test out things you will need to install doxygen.
Installation instructions can be found [here](https://www.doxygen.nl/download.html) (scroll down to "Sources and Binaries")

After having installed doxygen you can generate the documentation by running:
```shell
doxygen doxygen-config.toml
```

The documentation will be put into a gitignored `docs` folder
You can go into `docs/html/index.html` and serve this page to see the documentation built locally.

If you run Rider, you can serve the website right from your editor by opening the file and clicking the browser icons on the top right

![Serving websites in Rider](./serve-website.png)


### Before submitting a PR
- Please run `dotnet format` before submitting your pull requests.

# Release Process
Nuget releases happen automatically when a new Github release is made. The `release-drafter` github action should automatically generate release notes if PR's have been labelled correctly.

Each merge into the `main` branch triggers a release of a Github Packages package.

We automatically build the documentation based on some written docs and the source code.
You can find the main page of the generated documentation [here.](https://scadaminds.github.io/swiss-army-knife/index.html)
It is automatically published from the `main` branch, so it might be slightly ahead of the current Nuget release.

