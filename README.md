# SNBT-Parser
A C# library for turning raw stringified NBT into an easy to navigate object model.

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
tag.TryAs(out NbtCompound compound) //Returns true if tag is the given type and outputs as the given type if so.
tag.TryAs<NbtCompound>(out var compound) //Both the generic parameter and the out variable type can infer types from each other, you only have to specify one.
```
The available tag types are:
|Type|Description|
|---|---|
|`NbtPrimitive`|Used to represent all simple values like booleans, numbers and strings.|
|`NbtPrimitive<T>`|A subclass of `NbtPrimitive`, useful if you want to enforce a exact type of value. Often it's easier to use the parent class, since value conversions can help you get the desired value type regardless of internal value type.|
|`NbtArray`|Used to represent lists and arrays of any type|
|`NbtCompound`|Used to represent compounds/objects|

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
string value4 = primitive.ValueAs<bool>(); //EXCEPTION THROWN!
```
The following table describes the available value types:
|SNBT Type|C# Type|
|---|---|
|`bool`|`bool`|
|`byte`|**`sbyte`** :warning:|
|`short`|`short`|
|`int`|`int`|
|`long`|`long`|
|`float`|`float`|
|`double`|`double`|
|`string`|`string`|

The `NbtPrimitive` does not contain a `ValueIs` method, since most conversions have to be done in order to know if it was possible. To keep code efficient, the `TryValueAs` method is enforced instead. Example:
```C#
//Not possible
//if (primitive.ValueIs<string>()) DoSomething(primitive.ValueAs<string>());
//Correct way
if (primitive.TryValueAs(out string value)) DoSomething(value);
//If you are 100% sure you won't need the value.
if (primitive.TryValueAs(out _)) DoSomething();
```
:warning: Other `NbtTag` types also won't contain any shortcut methods related to *only* checking if the value matches a type.
