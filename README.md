# SNBT-Parser
**A C# library for turning raw stringified NBT into an easy to navigate object model.**

This guide functions as a quick start / quick reference. View the documentation in the code for more information.

## Parsing
```C#
var nbt = "{message:\"Hello World!\"}";
var parser = new NbtTagParser();
var tag = parser.Parse(nbt);
```

## NbtTag
The `NbtTag` is the base class for all other tag types. It contains several useful methods to analyze and downcast the tag.
```C#
NbtTag tag;
tag.Is<NbtPrimitive>(); //Returns true if tag is the given type.
tag.As<NbtCompound>(); //Returns the tag as the given type or throws an exception if the tag could not be returned as the given type.
tag.TryAs(out NbtCompound compound) //Returns true if tag is the given type and outputs the tag as the given type if so.
tag.TryAs<NbtCompound>(out var compound) //Alternative notation, you only need to specify the result type once.
```
The available tag types are:
|Type|Description|
|---|---|
|[`NbtPrimitive`](#nbtprimitive)|Used to represent all simple values like booleans, numbers and strings.|
|`NbtPrimitive<T>`|A subclass of `NbtPrimitive`, useful if you want to enforce an exact type of value. Often it's easier to use the parent class, since value conversions can help you get the desired value type regardless of internal value type.|
|[`NbtArray`](#nbtarray)|Used to represent lists and arrays of any type|
|[`NbtCompound`](#nbtcompound)|Used to represent compounds/objects|

## NbtPrimitive
The `NbtPrimitive` contains simple values.
```C#
NbtPrimitive primitive;
primitive.ValueIsExact<R>(); // Returns true if the internal value is exactly R. (Regardless of available conversions)
primitive.ValueAs<R>(); // Returns the internal value as R (Conversion applies, throws exception if no conversion is found).
primitive.TryValueAs(out R value); //Returns true if the internal value can be converted to R and if so, the value as R.
```
Example:
```C#
NbtPrimitive primitive = new NbtTagParser().Parse("1b").As<NbtPrimitive>();
int value1 = primitive.ValueAs<int>(); //1
sbyte value2 = primitive.ValueAs<sbyte>(); //1
bool value3 = primitive.ValueAs<bool>(); //true
string value4 = primitive.ValueAs<string>(); //EXCEPTION THROWN!
```
The following table describes the available value types:
|SNBT Type|`bool`|`byte`|`short`|`int`|`long`|`float`|`double`|`string`|
|---|---|---|---|---|---|---|---|---|
|C# Type|`bool`|**`sbyte`** :warning:|`short`|`int`|`long`|`float`|`double`|`string`|

⚠️ Additional notes:
* It's more efficient to retrieve a value as the internal type, since it doesn't require conversion. Try to retrieve items as their intended type (according to Minecraft wiki pages), since it has the highest chance of matching the internal type and not requiring value conversion.  
* JSON values (like raw text found in signs/books/tellraw command) are `NbtPrimitives` of type `string`, not `NbtCompounds`. You can pass a retrieved JSON string to the [`System.Text.Json.JsonDocument.Parse(...)`](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter) method for further parsing.

* The `NbtPrimitive` does not contain a `ValueIs` method, since most conversions have to be done in order to know if it was possible. To keep code efficient, the `TryValueAs` method is enforced instead. Example:
  ```C#
  //Not possible
  //if (primitive.ValueIs<string>()) DoSomething(primitive.ValueAs<string>());
  //Correct way
  if (primitive.TryValueAs(out string value)) DoSomething(value);
  //If you are 100% sure you won't need the value.
  if (primitive.TryValueAs(out _)) DoSomething();
  ```
  Other `NbtTag` types also won't contain any shortcut methods related to *only* checking if the value matches a type.

## NbtArray
The `NbtArray` can contain multiple other `NbtTags`. An array can only contain items of the same type, that's why item retrieval and downcast methods are for all items at once. Besides some methods to edit the array, the following methods are available:
```C#
NbtArray array;
array.ItemsAre<T>(); //Checks if the items could be returned as an NbtTag of type T.
array.ItemsAs<T>(); //Returns all items as IList<T>, where T is an NbtTag type. Throws an exception if items could not be returned as T.
array.TryItemsAs<T>(out var result); //Returns true if the items could be returned as an NbtTag of type T and if so, fills the result variable with the items.
array.ContainsItem(item); //Returns true if the array contains the item.
array.IndexOfItem(item); //Returns the index of the item or -1 if not found.
```
The above methods are for `NbtTag` types, but if you want to retrieve the values inside an `NbtPrimitive` you can use the available shortcut methods.
```C#
NbtArray array;
//array.ValuesAre<T>(); Doesn't exist for the reasons described in the NbtPrimitive section.
array.ValuesAs<T>(); //Returns all items as IList<T>, where T is a value type that is used in an NbtPrimitive. Throws an exception if the items are not primitives or the value inside could not be returned as T.
array.TryValuesAs<T>(out var result); //Returns true if the items are of type NbtPrimitive and the values inside could be returned as T and if so, fills the result variable with the values of the primitives.
array.ContainsValue(value); //Returns true if the array contains an NbtPrimitive with the specified value.
array.IndexOfValue(value); //Returns the index of the first NbtPrimitive with the specified value or -1 if not found.
```
:warning: The result of downcast/conversion methods should be reused as much as possible, since the methods are quite expensive.

## NbtCompound
The `NbtCompound` contains values associated with string keys. Besides some methods you would usually find on a `Dictionary`, the following methods are available for checking and retrieving items:
```C#
NbtCompound compound;
compound.ItemIs<T>(key); //Returns true if the item can be returned as an NbtTag of type T.
compound.ItemAs<T>(key); //Returns the item as T or throws an exception if that was not possible.
compound.TryItemAs<T>(key, out T item); //Returns true if the item can be returned as an NbtTag of type T and if so, the item associated with the key. This method normally does not throw any exceptions, but extra optional parameters are available to make it throw exceptions if the key is not found and/or the item can not be returned as T.
compound.ItemsAs<T>(); //Returns an IEnumerable<KeyValuePair<string, T>> which can be used to iterate over the compound.
```
There are additional shortcut methods available if you want to get the internal values of `NbtPrimitive` items.
```C#
NbtCompound compound;
//compound.ValueIs<T>(key); Doesn't exist for the reasons described in the NbtPrimitive section.
compound.ValueAs<T>(key); //Retrieves the item as NbtPrimitive and returns the value inside as T. Throws an exception if the value could not be returned correctly.
compound.TryValueAs<T>(key, out T item); //Returns true if the item is an NbtPrimitive and the value inside can be returned as T and if so, the value associated with the key. See above for an explanation on extra optional parameters.
compound.ValuesAs<T>(); //Returns an IEnumerable<KeyValuePair<string, T>> containing the internal values of NbtPrimitive items which can be used to iterate over the compound.
```
