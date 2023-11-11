# hash-based-search

A small package of extension methods for generic collections
to reduce the amount of code required to organize a readonly dictionary lookup
by constructing appropriate delegates.

Dictionary is totally readonly since nothing except constructed delegate has access to it.

Supports existing methods of constructing a dictionary based on .ToDictionary overloads.
Supports returning a preset default value if the searched element is missing.
Supports building search delegate from collection of KeyValuePair's.
Supports building search delegate from another Dictionary (by creating new one based on existing).

Benchmarking of some methods presented below.
SimpleSearch is implementation of linear search,
DictionarySearch~ presents common approach
and HashBasedSearch~ presents library usage.

The performance overhead and a few extra bytes allocated are a small price to pay
for reducing the amount of code required to perform a dictionary lookup.

| Method                                            | Count |            Mean |         Error |        StdDev |          Median |    Gen0 |   Gen1 | Allocated |
|---------------------------------------------------|-------|----------------:|--------------:|--------------:|----------------:|--------:|-------:|----------:|
| SimpleSearch                                      | 1     |       122.60 ns |      2.473 ns |      3.625 ns |       120.68 ns |  0.0477 |      - |     400 B |
| DictionarySearch                                  | 1     |       142.84 ns |      0.624 ns |      0.584 ns |       142.80 ns |  0.0591 |      - |     496 B |
| HashBasedSearch                                   | 1     |       157.83 ns |      0.454 ns |      0.403 ns |       157.77 ns |  0.0696 |      - |     584 B |
| DictionarySearchWithDefaultValue                  | 1     |       151.21 ns |      0.733 ns |      0.612 ns |       151.19 ns |  0.0629 |      - |     528 B |
| HashBasedSearchWithDefaultValue                   | 1     |       167.69 ns |      0.608 ns |      0.539 ns |       167.57 ns |  0.0744 |      - |     624 B |
| DictionarySearchOnEmptyCollection                 | 1     |        25.93 ns |      0.112 ns |      0.099 ns |        25.90 ns |  0.0181 |      - |     152 B |
| HashBasedSearchOnEmptyCollection                  | 1     |        55.44 ns |      1.020 ns |      1.703 ns |        54.67 ns |  0.0268 |      - |     224 B |
| DictionarySearchOnEmptyCollectionWithDefaultValue | 1     |        29.63 ns |      0.412 ns |      0.366 ns |        29.61 ns |  0.0220 |      - |     184 B |
| HashBasedSearchOnEmptyCollectionWithDefaultValue  | 1     |        62.95 ns |      1.270 ns |      1.247 ns |        62.71 ns |  0.0391 |      - |     328 B |
| SimpleSearch                                      | 10    |       967.43 ns |     19.088 ns |     19.602 ns |       966.13 ns |  0.2747 |      - |    2304 B |
| DictionarySearch                                  | 10    |       614.57 ns |     12.054 ns |     18.042 ns |       607.08 ns |  0.1841 |      - |    1544 B |
| HashBasedSearch                                   | 10    |       619.48 ns |      3.206 ns |      2.842 ns |       620.37 ns |  0.1945 |      - |    1632 B |
| DictionarySearchWithDefaultValue                  | 10    |       583.58 ns |      4.906 ns |      4.349 ns |       583.49 ns |  0.1879 |      - |    1576 B |
| HashBasedSearchWithDefaultValue                   | 10    |       645.44 ns |      4.679 ns |      4.147 ns |       644.39 ns |  0.1993 | 0.0010 |    1672 B |
| DictionarySearchOnEmptyCollection                 | 10    |        51.61 ns |      0.540 ns |      0.479 ns |        51.60 ns |  0.0220 |      - |     184 B |
| HashBasedSearchOnEmptyCollection                  | 10    |       105.17 ns |      0.421 ns |      0.352 ns |       105.12 ns |  0.0305 |      - |     256 B |
| DictionarySearchOnEmptyCollectionWithDefaultValue | 10    |        52.94 ns |      0.293 ns |      0.260 ns |        52.86 ns |  0.0258 |      - |     216 B |
| HashBasedSearchOnEmptyCollectionWithDefaultValue  | 10    |       113.58 ns |      0.598 ns |      0.499 ns |       113.73 ns |  0.0430 |      - |     360 B |
| SimpleSearch                                      | 100   |    36,248.96 ns |    366.396 ns |    324.801 ns |    36,244.19 ns |  2.5024 | 0.0610 |   21392 B |
| DictionarySearch                                  | 100   |     4,713.01 ns |     29.016 ns |     25.722 ns |     4,716.46 ns |  1.4954 | 0.0458 |   12520 B |
| HashBasedSearch                                   | 100   |     4,942.15 ns |     28.463 ns |     25.232 ns |     4,946.17 ns |  1.5030 | 0.0458 |   12608 B |
| DictionarySearchWithDefaultValue                  | 100   |     4,910.57 ns |     74.620 ns |     62.311 ns |     4,898.07 ns |  1.4954 | 0.0381 |   12552 B |
| HashBasedSearchWithDefaultValue                   | 100   |     4,921.69 ns |     20.437 ns |     18.117 ns |     4,920.32 ns |  1.5106 | 0.0381 |   12648 B |
| DictionarySearchOnEmptyCollection                 | 100   |       310.28 ns |      3.753 ns |      3.511 ns |       309.36 ns |  0.0648 |      - |     544 B |
| HashBasedSearchOnEmptyCollection                  | 100   |       608.33 ns |     11.881 ns |     13.682 ns |       601.14 ns |  0.0734 |      - |     616 B |
| DictionarySearchOnEmptyCollectionWithDefaultValue | 100   |       298.05 ns |      1.604 ns |      1.783 ns |       297.66 ns |  0.0687 |      - |     576 B |
| HashBasedSearchOnEmptyCollectionWithDefaultValue  | 100   |       600.05 ns |      4.387 ns |      4.104 ns |       597.79 ns |  0.0858 |      - |     720 B |
| SimpleSearch                                      | 1000  | 2,848,901.80 ns | 13,073.084 ns | 10,916.618 ns | 2,844,087.50 ns | 23.4375 | 3.9063 |  219395 B |
| DictionarySearch                                  | 1000  |    46,708.53 ns |    166.698 ns |    147.774 ns |    46,745.94 ns | 15.5640 | 3.1128 |  130408 B |
| HashBasedSearch                                   | 1000  |    51,959.54 ns |    455.741 ns |    426.301 ns |    51,748.08 ns | 15.5640 | 2.9297 |  130496 B |
| DictionarySearchWithDefaultValue                  | 1000  |    47,652.10 ns |    290.808 ns |    242.838 ns |    47,588.76 ns | 15.5640 | 3.4180 |  130440 B |
| HashBasedSearchWithDefaultValue                   | 1000  |    50,169.51 ns |    963.704 ns |    946.486 ns |    49,710.60 ns | 15.5640 | 2.9297 |  130536 B |
| DictionarySearchOnEmptyCollection                 | 1000  |     2,724.62 ns |     10.895 ns |     10.191 ns |     2,724.94 ns |  0.4921 |      - |    4144 B |
| HashBasedSearchOnEmptyCollection                  | 1000  |     5,133.23 ns |     12.984 ns |     10.137 ns |     5,133.24 ns |  0.5035 |      - |    4216 B |
| DictionarySearchOnEmptyCollectionWithDefaultValue | 1000  |     2,641.88 ns |      9.534 ns |      8.451 ns |     2,640.72 ns |  0.4959 |      - |    4176 B |
| HashBasedSearchOnEmptyCollectionWithDefaultValue  | 1000  |     5,121.14 ns |     26.226 ns |     20.476 ns |     5,113.87 ns |  0.5112 |      - |    4320 B |

Common usage is having some collection, for example:

```cs
record Entity(long Id, string Name, int SomeValue);

IEnumerable<Entity> entities;

void TestOne()
{
    HashBasedSearchCallback<long, Entity?> searchEntityCallBack =
        entities.BuildHashBasedSearchCallback(keySelector: entity => entity.Id);
    
    //result is nullable since collection element is nullable and no default value presented
    Entity? searchedEntity = searchEntityCallBack(key: 10);
}

void TestTwo()
{
    HashBasedSearchCallback<long, int?> searchValueCallBack = 
        entities.BuildHashBasedSearchCallbackNullable(
            keySelector: entity => entity.Id,
            elementSelector: entity => entity.SomeValue);
    
    //result is nullable since we use ~Nullable extension on not-nullable resulting element.
    int? searchedValue = searchValueCallBack(key: 10);
    if (searchedValue is null)
    {
        /*do some other work;*/
    }
}

void TestThree()
{
    HashBasedSearchCallback<long, string> searchNameCallBack =
        entities.BuildHashBasedSearchCallback(
            keySelector: entity => entity.Id,
            elementSelector: entity => entity.Name,
            defaultValue: "Name is unknown");
    
    //nullable type result is never null if default value is presented.
    string searchedName = searchNameCallBack(key: 10)!;
    Debug.Assert(searchedName is not null);
}
```