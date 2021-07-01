# SwissArmyKnife
![Banner](Images/scm_banner.png)

[![GitHub Actions Status](https://github.com/SCADAMINDS/swiss-army-knife/workflows/Build/badge.svg?branch=main)](https://github.com/SCADAMINDS/swiss-army-knife/actions)

[![GitHub Actions Build History](https://buildstats.info/github/chart/SCADAMINDS/swiss-army-knife?branch=main&includeBuildsFromPullRequest=false)](https://github.com/SCADAMINDS/swiss-army-knife/actions)

This is a project full of utility methods that can be used across multiple projects.  
The code is:
- [Thoroughly documented](https://scadaminds.github.io/swiss-army-knife/)
- Thoroughly tested
- Comprehensively null-annotated
- Includes SourceLink so you can step into the source to debug
- Methods that are side-effect free are marked as `[Pure]`

# Installation
This library can be installed from Nuget or Github Packages.
It targets [netstandard 2.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.1.md),
and will require a version of JSON.NET installed in the `11.x` range. 


### Nuget
You can install it from nuget by running `dotnet add package SCM.SwissArmyKnife`

### Github
If you are developing the library or want the latest packages built from the `main` branch, you can get them from Github packages.
1. Add a `nuget.config` file to the root of your project.
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="github" value="https://nuget.pkg.github.com/SCADAMINDS/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="USERNAME" />
            <add key="ClearTextPassword" value="TOKEN" />
        </github>
    </packageSourceCredentials>
</configuration>
```
2. Replace USERNAME with your Github username and TOKEN with a personal access token.
3. Add the package: `dotnet add package SCM.SwissArmyKnife --version {VERSION} --prerelease`

## SwissArmyKnife.Extensions
A bunch of handy extension methods that you'll probably like to use!
Examples:
```csharp

// ------------- Dictionary.GetOr
myDictionary.GetOr("nonExistingKey", () => "myFallbackValue");


// ------------- Task.Select()
var enumerableTask = Task.FromResult(new int[]{1,2});

// Select to transform multiple values
// Alternative to (await enumerableTask).Select(i => i + 1)
await enumerableTask.Select(i => i + 1); // Returns [2,3]


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

[And many more!](https://scadaminds.github.io/swiss-army-knife/md_Documentation_Extensions.html)

## SwissArmyKnife.Files
Methods to operate on files:
```cs
// Get the current solution directory. It will start with the current working directory and traverse upwards until finding the solution directory.
DirectoryInfo solutionDir = FileLocator.TryGetSolutionDirectoryInfo();

// If you need to get a specific file based on a relative path from the solution directory you can do it like this.
FileInfo programFile = FileLocator.GetFileStartingAtSolutionDirectory("SourceProject", "Program.cs");
```

[And more!](https://scadaminds.github.io/swiss-army-knife/md_Documentation_Files.html)


## SwissArmyKnife.Compression
Methods to compress and decompress data using gzip:
```cs
// Will return the compressed byte array
byte[] myCompressedByteArray = Gzip.Compress(myByteArray);

// Will return the compressed string as byte array, using ASCII for encoding
byte[] myCompressedStringASCII = Gzip.Compress(myString, Encoding.ASCII);

// Will return the compressed string as byte array, using UTF8 for encoding
byte[] myCompressedStringUTF8 = Gzip.Compress(myString);

// Will return the decompressed byte array
byte[] myDecompressedByteArray = Gzip.Decompress(myCompressedByteArray);

// Will return the decompressed string using the provided encoding
string myDecompressedStringASCII = Gzip.DecompressToString(myCompressedStringASCII, Encoding.ASCII);

// Will return the decompressed string using the default (UTF8) encoding
string myDecompressedStringUTF8 = Gzip.DecompressToString(myCompressedStringUTF8);
```

[And more!](https://scadaminds.github.io/swiss-army-knife/md_Documentation_Compression.html)

## Documentation
You can view the documentation for the `main` branch [here.](https://scadaminds.github.io/swiss-army-knife/index.html)

# Contributing
If you'd like to contribute view the contribution docs [here](./contributing.md)

