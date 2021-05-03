# Files

## FileLocator
This static class gives you an easy way to locate files relative to the solution directory.

E.g. assuming you have a directory structure like this:
```
SolutionDirectory/
├── MySolution.sln
├── SourceProject/
│   ├── SourceProject.csproj
│   ├── Program.cs
└── TestProject/
    ├── TestProject.csproj
    └── TestFile.cs
```

You can use two methods. The first one:

```cs
// If the current working directory is "below" the "SolutionDirectory" this will return
// the "SolutionDirectory"
DirectoryInfo solutionDir = FileLocator.TryGetSolutionDirectoryInfo();

// If you want to start at a different path than the current working directory, you can:
DirectoryInfo solutionDir = FileLocator.TryGetSolutionDirectoryInfo("/my/start/path/");

// If you need to get a specific file based on a relative path from the solution directory you can do it like this.
FileInfo programFile = FileLocator.GetFileStartingAtSolutionDirectory("SourceProject", "Program.cs");
```

\ref SCM.SwissArmyKnife.Files.FileLocator "View More Documentation"
