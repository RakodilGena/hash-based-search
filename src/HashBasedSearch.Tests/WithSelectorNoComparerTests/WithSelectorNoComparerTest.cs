namespace HashBasedSearch.Tests.WithSelectorNoComparerTests;

[TestClass]
public class WithSelectorNoComparerTest
{
    [TestMethod]
    public void ClassWithNoDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50, nameTemplate: "string");
        HashBasedSearchCallback<int, string?> searchCallback = testData.BuildHashBasedSearchCallback(
            keySelector: testClass => testClass.Id, 
            elementSelector: testClass => testClass.Name);
        
        int existingId = 30;
        string expectedString = "string30";
        string? actualString = searchCallback(existingId);
        
        Assert.AreEqual(expectedString, actualString);
    }
    
    [TestMethod]
    public void ClassWithNoDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        HashBasedSearchCallback<int, string?> searchCallback = testData.BuildHashBasedSearchCallback(
            keySelector: testClass => testClass.Id, 
            elementSelector: testClass => testClass.Name);

        int notExistingId = 100;
        string? notExistingString = searchCallback(notExistingId);
        Assert.IsTrue(notExistingString is null);
    }
    
    
    [TestMethod]
    public void ClassWithDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50, nameTemplate: "string");
        string defaultString = "default";
        HashBasedSearchCallback<int, string?> searchCallback = testData.BuildHashBasedSearchCallback( 
            keySelector: testClass => testClass.Id, 
            elementSelector: testClass => testClass.Name, 
            defaultValue: defaultString);
        
        int existingId = 30;
        string expectedString = "string30";
        string? actualString = searchCallback(existingId);
        
        Assert.AreEqual(expectedString, actualString);
    }
    
    [TestMethod]
    public void ClassWithDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50, nameTemplate: "string");
        string defaultString = "default";
        HashBasedSearchCallback<int, string?> searchCallback = testData.BuildHashBasedSearchCallback( 
            keySelector: testClass => testClass.Id, 
            elementSelector: testClass => testClass.Name, 
            defaultValue: defaultString);
        
        int notExistingId = 100;
        string? defaultStringFromCallback = searchCallback(notExistingId);
        Assert.AreSame(expected: defaultString, actual: defaultStringFromCallback);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int multiplier = 10;

        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(
            keySelector: c => c, elementSelector: c => c * multiplier);
        
        int existingKey = 30;

        int expectedValue = existingKey * multiplier; 
        
        int actualValue = searchCallback(existingKey);
        
        Assert.AreEqual(expectedValue, actualValue);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTestNullable_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int multiplier = 10;

        HashBasedSearchCallback<int, int?> searchCallback = testData.BuildHashBasedSearchCallbackNullable(
            keySelector: c => c, elementSelector: c => c * multiplier);
        
        int existingKey = 30;

        int expectedValue = existingKey * multiplier; 
        
        int? actualValue = searchCallback(existingKey);
        
        Assert.AreEqual(expectedValue, actualValue);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int multiplier = 10;

        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(
            keySelector: c => c, elementSelector: c => c * multiplier);
        
        int notExistingKey = 100;
        
        int notExpectedValue = notExistingKey * multiplier; 
        
        int actualValue = searchCallback(notExistingKey);
        
        Assert.AreNotEqual(notExpectedValue, actualValue);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTestNullable_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int multiplier = 10;

        HashBasedSearchCallback<int, int?> searchCallback = testData.BuildHashBasedSearchCallbackNullable(
            keySelector: c => c, elementSelector: c => c * multiplier);
        
        int notExistingKey = 100;
        
        int notExpectedValue = notExistingKey * multiplier; 
        
        int? actualValue = searchCallback(notExistingKey);
        
        Assert.AreNotEqual(notExpectedValue, actualValue);
        Assert.AreEqual(null, actualValue);
    }
    
    
    [TestMethod]
    public void StructWithDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int multiplier = 10;

        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(
            keySelector: c => c, elementSelector: c => c * multiplier, defaultValue: -500);
        
        int existingKey = 30;

        int expectedValue = existingKey * multiplier; 
        
        int actualValue = searchCallback(existingKey);
        
        Assert.AreEqual(expectedValue, actualValue);
    }
    
    [TestMethod]
    public void StructWithDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int multiplier = 10;

        int defaultValue = -500;
        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(
            keySelector: c => c, elementSelector: c => c * multiplier, defaultValue);
        
        int notExistingKey = 100;
        
        int actualValue = searchCallback(notExistingKey);
        
        Assert.AreEqual(expected: defaultValue, actualValue);
    }
}