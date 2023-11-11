namespace HashBasedSearch.Tests.DictionaryNoComparerTests;

[TestClass]
public class DictionaryNoComparerTest
{
    [TestMethod]
    public void ClassWithNoDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var existingDictionary = testData.ToDictionary(keySelector: testClass => testClass.Id);

        HashBasedSearchCallback<int, TestClass?> searchCallback = existingDictionary.BuildHashBasedSearchCallback();

        int existingId = 30;
        TestClass? existingEntity = searchCallback(existingId);
        Assert.IsTrue(existingEntity is not null && existingEntity.Id == existingId);
    }

    [TestMethod]
    public void ClassWithNoDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var existingDictionary = testData.ToDictionary(keySelector: testClass => testClass.Id);

        HashBasedSearchCallback<int, TestClass?> searchCallback = existingDictionary.BuildHashBasedSearchCallback();

        int notExistingId = 100;
        TestClass? notExistingEntity = searchCallback(notExistingId);
        Assert.IsTrue(notExistingEntity is null);
    }


    [TestMethod]
    public void ClassWithDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var existingDictionary = testData.ToDictionary(keySelector: testClass => testClass.Id);
        TestClass defaultEntity = new TestClass(-1, "default");

        HashBasedSearchCallback<int, TestClass?> searchCallback =
            existingDictionary.BuildHashBasedSearchCallback(defaultValue: defaultEntity);

        int existingId = 30;
        TestClass? existingEntity = searchCallback(existingId);
        Assert.IsTrue(existingEntity is not null && existingEntity.Id == existingId);
    }

    [TestMethod]
    public void ClassWithDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var existingDictionary = testData.ToDictionary(keySelector: testClass => testClass.Id);
        TestClass defaultEntity = new TestClass(-1, "default");

        HashBasedSearchCallback<int, TestClass?> searchCallback =
            existingDictionary.BuildHashBasedSearchCallback(defaultValue: defaultEntity);

        int notExistingId = 100;
        TestClass? defaultEntityFromCallback = searchCallback(notExistingId);
        Assert.AreSame(expected: defaultEntity, actual: defaultEntityFromCallback);
    }

    [TestMethod]
    public void StructWithNoDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var existingDictionary = testData.ToDictionary(keySelector: num => num);

        HashBasedSearchCallback<int, int> searchCallback = existingDictionary.BuildHashBasedSearchCallback();
        int existingKey = 30;
        int existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }

    [TestMethod]
    public void StructWithNoDefaultTestNullable_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var existingDictionary = testData.ToDictionary(keySelector: num => num);

        HashBasedSearchCallback<int, int?> searchCallback = existingDictionary.BuildHashBasedSearchCallbackNullable();
        int existingKey = 30;
        int? existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }

    [TestMethod]
    public void StructWithNoDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var existingDictionary = testData.ToDictionary(keySelector: num => num);

        HashBasedSearchCallback<int, int> searchCallback = existingDictionary.BuildHashBasedSearchCallback();
        int notExistingKey = 100;

        int returnValue = searchCallback(notExistingKey);
        Assert.IsTrue(returnValue == default);
    }

    [TestMethod]
    public void StructWithNoDefaultTestNullable_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var existingDictionary = testData.ToDictionary(keySelector: num => num);

        HashBasedSearchCallback<int, int?> searchCallback = existingDictionary.BuildHashBasedSearchCallbackNullable();
        int notExistingKey = 100;

        int? returnValue = searchCallback(notExistingKey);
        Assert.AreEqual(null, returnValue);
    }


    [TestMethod]
    public void StructWithDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var existingDictionary = testData.ToDictionary(keySelector: num => num);

        HashBasedSearchCallback<int, int> searchCallback =
            existingDictionary.BuildHashBasedSearchCallback(defaultValue: -200);
        int existingKey = 30;
        int existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }

    [TestMethod]
    public void StructWithDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var existingDictionary = testData.ToDictionary(keySelector: num => num);

        int defaultValue = -200;
        HashBasedSearchCallback<int, int>
            searchCallback = existingDictionary.BuildHashBasedSearchCallback(defaultValue);
        int notExistingKey = 100;

        int returnValue = searchCallback(notExistingKey);
        Assert.IsTrue(returnValue == defaultValue);
    }
}