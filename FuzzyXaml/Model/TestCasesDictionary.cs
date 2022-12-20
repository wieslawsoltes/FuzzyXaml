using System.Collections.Generic;

namespace FuzzyXaml.Model;

public class TestCasesDictionary
{
    private readonly Dictionary<TestCasesKey, List<TestCase>> _testCasesDictionary = new ();

    public void Add(TestCasesKey key, TestCase testCase)
    {
        if (_testCasesDictionary.TryGetValue(key, out var testCases))
        {
            testCases.Add(testCase);
        }
        else
        {
            _testCasesDictionary[key] = new List<TestCase> {testCase};
        }
    }

    public List<TestCase>? Get(TestCasesKey key)
    {
        if (_testCasesDictionary.TryGetValue(key, out var testCases))
        {
            return testCases;
        }

        return null;
    }
}
