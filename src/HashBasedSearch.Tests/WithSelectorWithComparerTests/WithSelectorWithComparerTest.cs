using HashBasedSearch.Tests.TestData.Complex;

namespace HashBasedSearch.Tests.WithSelectorWithComparerTests;

[TestClass]
public class WithSelectorWithComparerTest
{
    [TestMethod]
    public void ClassWithNoDefaultTest1()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");
        HashBasedSearchCallback<TestComplexClassKey, string?> searchCallback =
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key,
                elementSelector: testClass => testClass.DataStrings[0], comparer: new TestComplexClassKeyComparer());

        TestComplexClass exisingEntity = testData[30];
        TestComplexClassKey key = exisingEntity.Key;
        string expectedString = exisingEntity.DataStrings[0];
        
        string? actualString = searchCallback(key);
        
        Assert.AreEqual(expectedString, actualString);
    }
    
    [TestMethod]
    public void ClassWithNoDefaultTest2()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");
        HashBasedSearchCallback<TestComplexClassKey, string?> searchCallback = 
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key,
                elementSelector: testClass => testClass.DataStrings[0], comparer: new TestComplexClassKeyComparer());

        TestComplexClassKey notExistingKey = new TestComplexClassKey(keyInt: 100, keyChar: (char)100);

        string? actualString = searchCallback(notExistingKey);
        
        Assert.IsTrue(actualString is null);
    }
    
    
    [TestMethod]
    public void ClassWithDefaultTest1()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");

        string defaultString = "default string";
        
        HashBasedSearchCallback<TestComplexClassKey, string> searchCallback = 
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key,
                elementSelector: testClass => testClass.DataStrings[0], comparer: new TestComplexClassKeyComparer(), defaultValue: defaultString);

        TestComplexClass exisingEntity = testData[30];
        TestComplexClassKey key = exisingEntity.Key;

        string expectedString = exisingEntity.DataStrings[0];
        
        string? actualString = searchCallback(key);
        
        Assert.AreEqual(expectedString, actualString);
    }
    
    [TestMethod]
    public void ClassWithDefaultTest2()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");

        string defaultString = "default string";

        HashBasedSearchCallback<TestComplexClassKey, string> searchCallback = 
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key,
                elementSelector: testClass => testClass.DataStrings[0], comparer: new TestComplexClassKeyComparer(), defaultValue: defaultString);

        TestComplexClassKey notExistingKey = new TestComplexClassKey(keyInt: 300, keyChar: (char)300);

        string? actualString = searchCallback(notExistingKey);
        
        Assert.AreEqual(defaultString, actualString);
    }
}