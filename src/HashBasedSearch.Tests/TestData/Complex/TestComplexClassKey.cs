namespace HashBasedSearch.Tests.TestData.Complex;

public class TestComplexClassKey
{
    public int KeyInt { get; }
    public char KeyChar { get; }

    public TestComplexClassKey(int keyInt, char keyChar)
    {
        KeyInt = keyInt;
        KeyChar = keyChar;
    }
}