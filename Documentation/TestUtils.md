# TestUtils

# TemporaryFileFixture
A fixture you can use in your tests to create a temporary 0-byte file in the temp location on your system.
When disposed, the file will automatically be deleted. 
This is for when you need to test something that relies on the file-system that you're unable to change.
If the code is under your control, consider using [System.IO.Abstractions](https://github.com/System-IO-Abstractions/System.IO.Abstractions) for an in-memory filesystem that is much easier to test up against.  

```csharp
// Create an dispose of temporary file
using var temporaryFile = TemporaryFileFixture.Create();

// Write to file, read from file, do whatever
FileStream fileStream = temporaryFile.FileInfo.OpenWrite();
```

\ref SCM.SwissArmyKnife.TestUtils.TemporaryFileFixture "View More Documentation"


# TemporaryDirectoryFixture
A fixture you can use in your tests to create a temporary directory at `{temporary_directory}/{guid}` in your system. 
When disposed, the directory and all of the content will automatically be deleted.
This is for when you need to test something that relies on the file-system that you're unable to change.
If the code is under your control, consider using [System.IO.Abstractions](https://github.com/System-IO-Abstractions/System.IO.Abstractions) for an in-memory filesystem that is much easier to test up against.

```csharp
// Create a temporary directory, and delete it recursively at the end of the block.
using var temporaryDirectory = TemporaryDirectoryFixture.Create();

// Get the path and do something with it.
var directoryPath = temporaryDirectory.DirectoryInfo.FullName
```

\ref SCM.SwissArmyKnife.TestUtils.TemporaryDirectoryFixture "View More Documentation"


# SameValueDictionary
Mocking dictionaries can be convoluted, so this is a read-only dictionary that will pretend any key exists in it, and return the default value that you provided in the constructor.
Add operations are no-ops. They will not fail, but they will not modify the dictionary.
Iterating over the dictionary will throw an error.
```csharp
// Creates a dictionary that always returns "defaultValue"
var dictionary = new SameValueDictionary<string, string>("defaultValue");
// Add operations are no-op
dictionary["someKey"] = "foo";
// Returns "defaultValue"
var value = dictionary["someKey"];
```

\ref SCM.SwissArmyKnife.TestUtils.SameValueDictionary "View More Documentation"
