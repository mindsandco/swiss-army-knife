# [Extensions](html/namespaceSCM_1_1SwissArmyKnife_1_1Extensions.html)

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
myDictionary.GetOrThrow("nonExistingKey", () => "myFallbackValue");
```

\ref SCM.SwissArmyKnife.Extensions.DictionaryExtensions "View More Documentation"

## EnumerableProducingExtensions
TODO

## FluentTaskExtensions
TODO

## HttpClientExtensions
TODO

## HttpResponseMessageExtensions
TODO

## MemoryStreamExtensions
TODO

## NullableExtensions
TODO

## ObjectExtensions
TODO

## RandomExtensions
TODO

## StreamExtensions
TODO

## StringExtensions
TODO
