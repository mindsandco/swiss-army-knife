# Extensions

## DictionaryExtensions

```{.cs}
var myDictionary = new Dictionary<string, string>; {
    {"myKey", "myValue"}
}

// Will return "myValue"
myDictionary.GetOrThrow("myKey", () => new ArgumentException("tried to get invalid key"));
// Will throw ArgumentException
myDictionary.GetOrThrow("nonExistingKey", () => new ArgumentException("tried to get invalid key"));

// Will return "myFallbackValue"
myDictionary.GetOr("nonExistingKey", () => "myFallbackValue");

var myDictionary = new Dictionary<string, string> {
    {"myKey", "2"}
}
 // Will return a new dictionary where values type is integer
 var convertedValueDictionary = myDictionary.ConvertValuesToNewType<string, string, int>("myKey", oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));
     
// Will throw InvalidOperationException
var myDictionary = new Dictionary<string, string> {
    {"myKey", "bar"}
}
var throwsOnConvertDictionary = myDictionary.ConvertValuesToNewType<string, string, int>("myKey", oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));
```

\ref SCM.SwissArmyKnife.Extensions.DictionaryExtensions "View More Documentation"

## EnumerableProducingExtensions

```{.cs}
int myItem = 3;

// Type: IEnumerable<int>
var myEnumerable = myItem.Yield();

// Type: IAsyncEnumerable<int>
var myAsyncEnumerable = myItem.YieldAsync();

```

\ref SCM.SwissArmyKnife.Extensions.EnumerableProducingExtensions "View More Documentation"


## FluentTaskExtensions
All tasks here are alternatives to calling `(await MyMethodAsync()).SomeFunction()` but instead allows you to chain the function
at the end, and await the whole chain.
```{.cs}
var singleValueTask = Task.FromResult(1);
var enumerableTask = Task.FromResult(new int[]{1,2});

// Select to transform a single value
await singleValueTask.Select(i => i + 1); // Returns 2

// Select to transform multiple values
// Alternative to (await enumerableTask).Select(i => i + 1)
await enumerableTask.Select(i => i + 1); // Returns [2,3]

// First
// Returns 1 
await enumerableTask.First();

// SelectMany
var taskThatEmitsMultipleEnumerables = Task.FromResult(new int[][]{ new int[]{1,2}, new int[]{3,4});
// Returns [1, 2, 3, 4]
await taskThatEmitsMultipleEnumerables.SelectMany(i => i)

// ToList - Converts an enumerable returning tasks to a List
List<int> list = await enumerableTask.ToList()
```

## HttpClientExtensions
```{.cs}
var client = new HttpClient();

var url = "http://www.some-url-that-produces-json.com"

// Gets URL and serializes model to MyResponseModel. On error prints http response
var await response = client.GetAsJsonAsync<MyResponseModel>(url)

// Gets URL and serializes model to MyResponseModel. On error prints http response up to a maximum of 100 chararcters
var await response = client.GetAsJsonAsync<MyResponseModel>(url, maxCharactersToPrint: 100)


var myModel = new MyDomainModel();

// POSTS URL with MyDomainModel as JSON body and serializes model to MyResponseModel.
var await response = client.PostAsJsonAsync<MyResponseModel>(url, myModel)
```

\ref SCM.SwissArmyKnife.Extensions.HttpClientExtensions "View More Documentation"


## HttpResponseMessageExtensions
```{.cs}
var client = new HttpClient();
HttpResponseMessage message = client.GetAsync("www.some-url.com"

await message.EnsureSuccessStatusCodeOrLogAsync((error, responseBody) => {
    // Before error is thrown you have a logging hook
    Console.Error(error, $"Error happened when making http request. Server responded with body {responseBody}");
});
```
\ref SCM.SwissArmyKnife.Extensions.HttpResponseMessageExtensions "View More Documentation"


## MemoryStreamExtensions
```{.cs}
// Assume you got myMemoryStream from somewhere

// clonedStream is now a copy of the entire stream from start-end
// the original memoryStream is still at the same position as before
var clonedStream = myMemoryStream.CloneEntireStream();
```

\ref SCM.SwissArmyKnife.Extensions.MemoryStreamExtensions "View More Documentation"


## NullableExtensions
```{.cs}
int? myNullableValue = 2;
int? myNullvalue = null;

// Returns 4
myNullableValue.TransformIfExists(i => i + 2);

// Returns null
myNullValue.TransformIfExists(i => i + 2);
```
\ref SCM.SwissArmyKnife.Extensions.NullableExtensions "View More Documentation"


## ObjectExtensions
```{.cs}
var myModel = new MyModel();

// Returns a JSON-string representation of the object.
// Enums are converted to string representations
string jsonString = myModel.ToJson();

// Same as above except that the JSON is indented and not compact
string indentedJsonString = myModel.ToIndentedJson();

// For debugging purposes. Prints the entire object as JSON via `Console.Writeline` 
myModel.PrintAsJson();
```

\ref SCM.SwissArmyKnife.Extensions.ObjectExtensions "View More Documentation"


## RandomExtensions
```{.cs}
var random = new Random();

// Returns a double between the two values
// E.g. 1234.34
double nextDouble = random.NextDouble(0, 500)

// Returns true or false with a 50% chance of each.
bool randomBoolean =  random.NextBoolean()

// Returns a random byte
byte randomBoolean =  random.NextByte()

// Returns a random choice from an enumerable.
var randomChoice = random.Choice(new []{"foo", "bar", "qux"}) 
```
\ref SCM.SwissArmyKnife.Extensions.RandomExtensions "View More Documentation"


## StreamExtensions
```{.cs}
var myStream = /* acquire stream somehow */

// Copies stream to a MemoryStream ready to be read
MemoryStream memoryStream = await myStream.AsMemoryStreamAsync();

// Copy stream to byte array
byte[] byteArray = await myStream.ToByteArrayAsync();

// Read stream as string
string stringFromStream = myStream.ContentToString()

// You can also specify encoding
string stringFromStream = myStream.ContentToString(Encoding.ASCII)
```
\ref SCM.SwissArmyKnife.Extensions.StreamExtensions "View More Documentation"


## StringExtensions

```{.cs}
var string = "hello";

// Returns "hellohello"
string.Repeat(2);

// Returns "hello"
string.Truncate(maxLength: 10);

// Returns "hel..."
string.Truncate(maxLength: 3);

// Returns true
string.IsSet();
```
\ref SCM.SwissArmyKnife.Extensions.StringExtensions "View More Documentation"
