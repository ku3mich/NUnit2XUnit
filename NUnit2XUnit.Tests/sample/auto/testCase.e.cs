public class TestClass
{
    [Theory]
    [InlineData("oops")]
    [InlineData("upps")]
    public void TestCase(string msg)
    {
    }
}

