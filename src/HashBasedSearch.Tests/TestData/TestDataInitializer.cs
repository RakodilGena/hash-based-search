namespace HashBasedSearch.Tests.TestData;

internal static class TestDataInitializer
{
    public static TestClass[] InitClassData(int count, string nameTemplate = "class")
    {
        TestClass[] data = Enumerable.Range(1, count).Select(id => new TestClass(id, $"{nameTemplate}{id}")).ToArray();
        return data;
    }

    public static int[] InitStructData(int count)
    {
        int[] data = Enumerable.Range(1, count).ToArray();
        return data;
    }
}