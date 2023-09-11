namespace HashBasedSearch.Tests.TestData.Complex;

public class TestComplexClassKeyComparer : IEqualityComparer<TestComplexClassKey>
{
    public bool Equals(TestComplexClassKey? first, TestComplexClassKey? second)
    {
        return first is not null && second is not null &&
               first.KeyInt == second.KeyInt && first.KeyChar == second.KeyChar;
    }

    public int GetHashCode(TestComplexClassKey x)
    {
        return x.KeyInt.GetHashCode() ^ x.KeyChar.GetHashCode();
    }
}