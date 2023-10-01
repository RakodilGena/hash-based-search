using HashBasedSearch.Tests.TestData.Complex;

namespace HashBasedSearch.Tests.NoSelectorWithComparerTests;

[TestClass]
public class NoSelectorWithComparerTest
{
    [TestMethod]
    public void ClassWithNoDefaultTest_Existing()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");
        HashBasedSearchCallback<TestComplexClassKey, TestComplexClass?> searchCallback = 
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key, comparer: new TestComplexClassKeyComparer());

        TestComplexClass exisingEntity = testData[30];
        TestComplexClassKey key = exisingEntity.Key;

        TestComplexClass? actualEntity = searchCallback(key);
        
        Assert.AreSame(expected: exisingEntity, actualEntity);
    }
    
    [TestMethod]
    public void ClassWithNoDefaultTest_NotExisting()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");
        HashBasedSearchCallback<TestComplexClassKey, TestComplexClass?> searchCallback = 
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key, comparer: new TestComplexClassKeyComparer());

        TestComplexClassKey notExistingKey = new TestComplexClassKey(keyInt: 100, keyChar: (char)100);

        TestComplexClass? actualEntity = searchCallback(notExistingKey);
        
        Assert.IsTrue(actualEntity is null);
    }
    
    
    [TestMethod]
    public void ClassWithDefaultTest_Existing()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");

        TestComplexClass defaultEntity =
            new TestComplexClass(new TestComplexClassKey(500, (char)500), Array.Empty<string>());
        
        HashBasedSearchCallback<TestComplexClassKey, TestComplexClass?> searchCallback = 
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key, comparer: new TestComplexClassKeyComparer(), defaultValue: defaultEntity);

        TestComplexClass exisingEntity = testData[30];
        TestComplexClassKey key = exisingEntity.Key;

        TestComplexClass? actualEntity = searchCallback(key);
        
        Assert.AreSame(expected: exisingEntity, actualEntity);
    }
    
    [TestMethod]
    public void ClassWithDefaultTest_NotExisting()
    {
        TestComplexClass[] testData = TestDataInitializer.InitComplexClassData(50, dataCount: 10, dataTemplate: "string");

        TestComplexClass defaultEntity =
            new TestComplexClass(new TestComplexClassKey(500, (char)500), Array.Empty<string>());
        HashBasedSearchCallback<TestComplexClassKey, TestComplexClass?> searchCallback = 
            
            testData.BuildHashBasedSearchCallback(keySelector: testClass => testClass.Key, comparer: new TestComplexClassKeyComparer(), defaultValue: defaultEntity);

        TestComplexClassKey notExistingKey = new TestComplexClassKey(keyInt: 300, keyChar: (char)300);

        TestComplexClass? actualEntity = searchCallback(notExistingKey);
        
        Assert.AreSame(expected: defaultEntity, actualEntity);
    }
}