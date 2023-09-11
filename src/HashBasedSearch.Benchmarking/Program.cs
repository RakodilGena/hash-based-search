
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using HashBasedSearch;

var summary = BenchmarkRunner.Run<BenchmarkHashBasedSearch>();


[MemoryDiagnoser(true)]
public class BenchmarkHashBasedSearch
{
    [Params(10, 100, 1000)]
    public int Count { get; set; }

    private (SimpleEntity[], int[]) BuildTestCollection()
    {
        int[] keys = Enumerable.Range(1, Count).ToArray();
        SimpleEntity[] arr = keys.Select(id => new SimpleEntity(id, $"entityName-{id}")).ToArray();

        return (arr, keys);
    }

    [Benchmark]
    public void SimpleSearch()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        foreach (int key in keys)
        {
            SimpleEntity foundEntity = collection.First(entity => entity.Id == key);
        }
    }
    
    [Benchmark]
    public void DictionarySearch()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        Dictionary<int, SimpleEntity> dict = collection.ToDictionary(keySelector: entity => entity.Id);

        foreach (int key in keys)
        {
            bool success = dict.TryGetValue(key, out SimpleEntity? foundEntity);
        }
    }

    [Benchmark]
    public void HashBasedSearch()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        var searchCallback = collection.BuildHashBasedSearchCallback(keySelector: entity => entity.Id);

        foreach (int key in keys)
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

        foreach (int key in keys)
        {
            bool success = dict.TryGetValue(key, out SimpleEntity? foundEntity);
            foundEntity ??= defaultEntity;
        }
    }

    [Benchmark]
    public void HashBasedSearchWithDefaultValue()
    {
        (SimpleEntity[] collection, int[] keys) = BuildTestCollection();

        var searchCallback = collection.BuildHashBasedSearchCallback(keySelector: entity => entity.Id,
            defaultValue: new SimpleEntity(-1, "default"));

        foreach (int key in keys)
        {
            SimpleEntity foundEntity = searchCallback(key)!;
        }
    }
}


class SimpleEntity
{
    public int Id { get; }
    
    public string Name { get; }

    public SimpleEntity(int id, string name)
    {
        Id = id;
        Name = name;
    }
}