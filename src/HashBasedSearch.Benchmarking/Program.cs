using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HashBasedSearch;

var summary = BenchmarkRunner.Run<BenchmarkHashBasedSearch>();


[MemoryDiagnoser(true)]
public class BenchmarkHashBasedSearch
{
    [Params(1, 10, 100, 1000)]
    public int Count { get; set; }

    private (SimpleEntity[], int[]) BuildTestCollection()
    {
        int[] keys = Enumerable.Range(1, Count).ToArray();
        SimpleEntity[] arr = keys.Select(id => new SimpleEntity(id, $"entityName-{id}")).ToArray();

        return (arr, keys);
    }

    private IEnumerable<int> TransformKeys(IEnumerable<int> keys)
        => keys.Select(key => key * 2);

    [Benchmark]
    public void SimpleSearch()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity? foundEntity = collection.FirstOrDefault(entity => entity.Id == key);
        }
    }
    
    [Benchmark]
    public void DictionarySearch()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        Dictionary<int, SimpleEntity> dict = collection.ToDictionary(keySelector: entity => entity.Id);

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity? foundEntity = GetSimpleEntityFromDictionary(dict, key);
        }
    }

    private SimpleEntity? GetSimpleEntityFromDictionary(Dictionary<int, SimpleEntity> dict, int key)
    {
        bool success = dict.TryGetValue(key, out SimpleEntity? foundEntity);
        return success ? foundEntity : null;
    }

    [Benchmark]
    public void HashBasedSearch()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        var searchCallback = collection.BuildHashBasedSearchCallback(keySelector: entity => entity.Id);

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity? foundEntity = searchCallback(key);
        }
    }

    [Benchmark]
    public void DictionarySearchWithDefaultValue()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        Dictionary<int, SimpleEntity> dict = collection.ToDictionary(keySelector: entity => entity.Id);
        SimpleEntity defaultEntity = new SimpleEntity(-1, "default");

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity foundEntity = GetSimpleEntityFromDictionary(dict, key, defaultEntity);
        }
    }
    
    private SimpleEntity GetSimpleEntityFromDictionary(Dictionary<int, SimpleEntity> dict, int key, SimpleEntity defaultEntity)
    {
        bool success = dict.TryGetValue(key, out SimpleEntity? foundEntity);
        return success ? foundEntity! : defaultEntity;
    }

    [Benchmark]
    public void HashBasedSearchWithDefaultValue()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        var searchCallback = collection.BuildHashBasedSearchCallback(keySelector: entity => entity.Id,
            defaultValue: new SimpleEntity(-1, "default"));

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity foundEntity = searchCallback(key)!;
        }
    }

    [Benchmark]
    public void DictionarySearchOnEmptyCollection()
    {
        int[] keys = Enumerable.Range(1, Count).ToArray();

        Dictionary<int, SimpleEntity> dict = Enumerable.Empty<SimpleEntity>().ToDictionary(keySelector: entity => entity.Id);

        foreach (int key in keys)
        {
            SimpleEntity? foundEntity = GetSimpleEntityFromDictionary(dict, key);
        }
    }
    
    [Benchmark]
    public void HashBasedSearchOnEmptyCollection()
    {
        int[] keys = Enumerable.Range(1, Count).ToArray();

        var searchCallback = Enumerable.Empty<SimpleEntity>().BuildHashBasedSearchCallback(keySelector: entity => entity.Id);

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity? foundEntity = searchCallback(key);
        }
    }
    
    
    [Benchmark]
    public void DictionarySearchOnEmptyCollectionWithDefaultValue()
    {
        int[] keys = Enumerable.Range(1, Count).ToArray();

        Dictionary<int, SimpleEntity> dict = Enumerable.Empty<SimpleEntity>().ToDictionary(keySelector: entity => entity.Id);
        SimpleEntity defaultEntity = new SimpleEntity(-1, "default");
        
        foreach (int key in keys)
        {
            SimpleEntity foundEntity = GetSimpleEntityFromDictionary(dict, key, defaultEntity);
        }
    }
    
    [Benchmark]
    public void HashBasedSearchOnEmptyCollectionWithDefaultValue()
    {
        int[] keys = Enumerable.Range(1, Count).ToArray();

        var searchCallback = Enumerable.Empty<SimpleEntity>().BuildHashBasedSearchCallback(keySelector: entity => entity.Id,
            defaultValue: new SimpleEntity(-1, "default"));

        foreach (int key in TransformKeys(keys))
        {
            SimpleEntity foundEntity = searchCallback(key)!;
        }
    }
    
}


sealed class SimpleEntity
{
    public int Id { get; }
    
    public string Name { get; }

    public SimpleEntity(int id, string name)
    {
        Id = id;
        Name = name;
    }
}


// class BasicUsage
// {
//     record Entity(long Id, string Name, int SomeValue);
//
//     public void E()
//     {
//         var entities = new Entity[50];
//         {
//             HashBasedSearchCallback<long, Entity?> searchEntityCallBack =
//                 entities.BuildHashBasedSearchCallback(keySelector: entity => entity.Id);
//             //result is nullable since collection element is nullable and no default value presented
//             Entity? searchedEntity = searchEntityCallBack(key: 10);
//         }
//
//         {
//             HashBasedSearchCallback<long, int?> searchValueCallBack = entities.BuildHashBasedSearchCallbackNullable(
//                 keySelector: entity => entity.Id,
//                 elementSelector: entity => entity.SomeValue);
//             //result is nullable since we use ~Nullable extension on not-nullable resulting element. 
//             int? searchedValue = searchValueCallBack(key: 10);
//             if (searchedValue is null)
//             {
//                 /*do some other work;*/
//             }
//         }
//
//         {
//             HashBasedSearchCallback<long, string> searchNameCallBack = entities.BuildHashBasedSearchCallback(
//                 keySelector: entity => entity.Id,
//                 elementSelector: entity => entity.Name,
//                 defaultValue: "Name is unknown");
//             //nullable type result is never null if default value is presented.
//             string searchedName = searchNameCallBack(key: 10)!;
//             Debug.Assert(searchedName is not null);
//         }
//
//
//     }
// }