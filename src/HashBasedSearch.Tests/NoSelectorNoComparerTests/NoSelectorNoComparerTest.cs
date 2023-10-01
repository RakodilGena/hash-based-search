namespace HashBasedSearch.Tests.NoSelectorNoComparerTests;

[TestClass]
public class NoSelectorNoComparerTest
{
    [TestMethod]
    public void ClassWithNoDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        HashBasedSearchCallback<int, TestClass?> searchCallback = testData.BuildHashBasedSearchCallback(testClass => testClass.Id);
        
        int existingId = 30;
        TestClass? existingEntity = searchCallback(existingId);
        Assert.IsTrue(existingEntity is not null && existingEntity.Id == existingId);
    }
    
    [TestMethod]
    public void ClassWithNoDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        HashBasedSearchCallback<int, TestClass?> searchCallback = testData.BuildHashBasedSearchCallback(testClass => testClass.Id);

        int notExistingId = 100;
        TestClass? notExistingEntity = searchCallback(notExistingId);
        Assert.IsTrue(notExistingEntity is null);
    }
    
    
    [TestMethod]
    public void ClassWithDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        TestClass defaultEntity = new TestClass(-1, "default");
        HashBasedSearchCallback<int, TestClass?> searchCallback = testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Id, defaultEntity);
        
        int existingId = 30;
        TestClass? existingEntity = searchCallback(existingId);
        Assert.IsTrue(existingEntity is not null && existingEntity.Id == existingId);
    }
    
    [TestMethod]
    public void ClassWithDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        TestClass defaultEntity = new TestClass(-1, "default");
        HashBasedSearchCallback<int, TestClass?> searchCallback = testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Id, defaultEntity);
        
        int notExistingId = 100;
        TestClass? defaultEntityFromCallback = searchCallback(notExistingId);
        Assert.AreSame(expected: defaultEntity, actual: defaultEntityFromCallback);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        
        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(c => c);
        int existingKey = 30;
        int existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTestNullable_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        
        HashBasedSearchCallback<int, int?> searchCallback = testData.BuildHashBasedSearchCallbackNullable(c => c);
        int existingKey = 30;
        int? existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        
        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(c => c);
        int notExistingKey = 100;
        
        int returnValue = searchCallback(notExistingKey);
        Assert.IsTrue(returnValue == default);
    }
    
    [TestMethod]
    public void StructWithNoDefaultTestNullable_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        HashBasedSearchCallback<int, int?> searchCallback = testData.BuildHashBasedSearchCallbackNullable(c => c);
        int notExistingKey = 100;
        
        int? returnValue = searchCallback(notExistingKey);
        Assert.AreEqual(null, returnValue);
    }
    
    
    [TestMethod]
    public void StructWithDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        
        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(keySelector: c => c, defaultValue: -200);
        int existingKey = 30;
        int existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }
    
    [TestMethod]
    public void StructWithDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);

        int defaultValue = -200;
        HashBasedSearchCallback<int, int> searchCallback = testData.BuildHashBasedSearchCallback(keySelector: c => c, defaultValue);
        int notExistingKey = 100;
        
        int returnValue = searchCallback(notExistingKey);
        Assert.IsTrue(returnValue == defaultValue);
    }
}