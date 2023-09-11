namespace HashBasedSearch.Tests.TestData.Complex;

public class TestComplexClass
{
   public TestComplexClassKey Key { get; }
    
    public string[] DataStrings { get; }

    public TestComplexClass(TestComplexClassKey key, string[] dataStrings)
    {
        Key = key;
        DataStrings = dataStrings;
    }
}