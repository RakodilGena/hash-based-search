namespace HashBasedSearch.Tests.KeyValuePairsNoComparerTests;

[TestClass]
public sealed class KeyValuePairsNoComparerTest
{
    [TestMethod]
    public void ClassWithNoDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var keyValuePairs = testData.Select(testClass =>
            new KeyValuePair<int, TestClass>(testClass.Id, testClass));

        HashBasedSearchCallback<int, TestClass?> searchCallback = keyValuePairs.BuildHashBasedSearchCallback();

        int existingId = 30;
        TestClass? existingEntity = searchCallback(existingId);
        Assert.IsTrue(existingEntity is not null && existingEntity.Id == existingId);
    }

    [TestMethod]
    public void ClassWithNoDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var keyValuePairs = testData.Select(testClass =>
            new KeyValuePair<int, TestClass>(testClass.Id, testClass));

        HashBasedSearchCallback<int, TestClass?> searchCallback = keyValuePairs.BuildHashBasedSearchCallback();

        int notExistingId = 100;
        TestClass? notExistingEntity = searchCallback(notExistingId);
        Assert.IsTrue(notExistingEntity is null);
    }


    [TestMethod]
    public void ClassWithDefaultTest_Existing()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var keyValuePairs = testData.Select(testClass =>
            new KeyValuePair<int, TestClass>(testClass.Id, testClass));
        TestClass defaultEntity = new TestClass(-1, "default");

        HashBasedSearchCallback<int, TestClass?> searchCallback =
            keyValuePairs.BuildHashBasedSearchCallback(defaultValue: defaultEntity);

        int existingId = 30;
        TestClass? existingEntity = searchCallback(existingId);
        Assert.IsTrue(existingEntity is not null && existingEntity.Id == existingId);
    }

    [TestMethod]
    public void ClassWithDefaultTest_NotExisting()
    {
        TestClass[] testData = TestDataInitializer.InitClassData(50);
        var keyValuePairs = testData.Select(testClass =>
            new KeyValuePair<int, TestClass>(testClass.Id, testClass));
        TestClass defaultEntity = new TestClass(-1, "default");

        HashBasedSearchCallback<int, TestClass?> searchCallback =
            keyValuePairs.BuildHashBasedSearchCallback(defaultValue: defaultEntity);

        int notExistingId = 100;
        TestClass? defaultEntityFromCallback = searchCallback(notExistingId);
        Assert.AreSame(expected: defaultEntity, actual: defaultEntityFromCallback);
    }

    [TestMethod]
    public void StructWithNoDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var keyValuePairs = testData.Select(num => new KeyValuePair<int, int>(num, num));

        HashBasedSearchCallback<int, int> searchCallback = keyValuePairs.BuildHashBasedSearchCallback();
        int existingKey = 30;
        int existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }

    [TestMethod]
    public void StructWithNoDefaultTestNullable_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var keyValuePairs = testData.Select(num => new KeyValuePair<int, int>(num, num));

        HashBasedSearchCallback<int, int?> searchCallback = keyValuePairs.BuildHashBasedSearchCallbackNullable();
        int existingKey = 30;
        int? existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }

    [TestMethod]
    public void StructWithNoDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var keyValuePairs = testData.Select(num => new KeyValuePair<int, int>(num, num));

        HashBasedSearchCallback<int, int> searchCallback = keyValuePairs.BuildHashBasedSearchCallback();
        int notExistingKey = 100;

        int returnValue = searchCallback(notExistingKey);
        Assert.IsTrue(returnValue == default);
    }

    [TestMethod]
    public void StructWithNoDefaultTestNullable_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var keyValuePairs = testData.Select(num => new KeyValuePair<int, int>(num, num));

        HashBasedSearchCallback<int, int?> searchCallback = keyValuePairs.BuildHashBasedSearchCallbackNullable();
        int notExistingKey = 100;

        int? returnValue = searchCallback(notExistingKey);
        Assert.AreEqual(null, returnValue);
    }


    [TestMethod]
    public void StructWithDefaultTest_Existing()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var keyValuePairs = testData.Select(num => new KeyValuePair<int, int>(num, num));

        HashBasedSearchCallback<int, int> searchCallback =
            keyValuePairs.BuildHashBasedSearchCallback(defaultValue: -200);
        int existingKey = 30;
        int existingValue = searchCallback(existingKey);
        Assert.IsTrue(existingKey == existingValue);
    }

    [TestMethod]
    public void StructWithDefaultTest_NotExisting()
    {
        int[] testData = TestDataInitializer.InitStructData(50);
        var keyValuePairs = testData.Select(num => new KeyValuePair<int, int>(num, num));

        int defaultValue = -200;
        HashBasedSearchCallback<int, int> searchCallback = keyValuePairs.BuildHashBasedSearchCallback(defaultValue);
        int notExistingKey = 100;

        int returnValue = searchCallback(notExistingKey);
        Assert.IsTrue(returnValue == defaultValue);
    }
}