using HashBasedSearch.Tests.TestData.Complex;

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
    
    public static TestComplexClass[] InitComplexClassData(int count, int dataCount = 5, string dataTemplate = "string")
    {
        if (count > 65535) count = 65535;
        
        
        TestComplexClass[] data = Enumerable.Range(1, count).Select(id => 
            new TestComplexClass(
                new TestComplexClassKey(
                    keyInt: id, 
                    keyChar: (char)id), 
                dataStrings: Enumerable.Range(1, dataCount).Select(dataId => $"{dataTemplate}-{id}-{dataId}").ToArray()))
            .ToArray();

        return data;
    }
}