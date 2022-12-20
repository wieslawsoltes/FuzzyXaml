namespace FuzzyXaml.Model;

public class TestCase
{
    public int Level { get; set; }

    public int Index { get; set; }

    public TestAction? Action { get; set; }

    public TestCase(int level, int index, TestAction? action)
    {
        Level = level;
        Index = index;
        Action = action;
    }
}
