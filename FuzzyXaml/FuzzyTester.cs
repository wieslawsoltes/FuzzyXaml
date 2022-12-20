using System.Linq;
using Avalonia.Controls;
using Avalonia.VisualTree;
using FuzzyXaml.Model;
using FuzzyXaml.Model.Actions;

namespace FuzzyXaml;

public static class FuzzyTester
{
    public static TestCasesDictionary GenerateTestCases(this IVisual visual)
    {
        var testCasesDictionary = new TestCasesDictionary();
        var level = 0;
        var controls = visual.GetVisualDescendants().OfType<Control>().ToList();

        for (var index = 0; index < controls.Count; index++)
        {
            var control = controls[index];
            var key = new TestCasesKey(level, index);

            // Console.WriteLine($"[{level}:{index}] {control.GetType().FullName}");

            switch (control)
            {
                case TextBox:
                {
                    // Console.WriteLine($"[{nameof(TextBox)}.{nameof(TextBox.Text)}]");
                    testCasesDictionary.Add(key, new TestCase(level, index, new TextBoxSetTextPropertyTestAction("Random Text")));
                    break;
                }
                case Button:
                {
                    // Console.WriteLine($"[{nameof(Button)}.{nameof(Button.Command)}]");
                    testCasesDictionary.Add(key, new TestCase(level, index, new ButtonExecuteCommandTestAction(null)));
                    break;
                }
            }
        }

        return testCasesDictionary;
    }

    public static void ExecuteTestCases(this IVisual visual, TestCasesDictionary testCasesDictionary)
    {
        var level = 0;
        var controls = visual.GetVisualDescendants().OfType<Control>().ToList();

        for (var index = 0; index < controls.Count; index++)
        {
            var control = controls[index];
            var key = new TestCasesKey(level, index);

            // Console.WriteLine($"[{level}:{index}] {control.GetType().FullName}");

            var testCases = testCasesDictionary.Get(key);
            if (testCases is { })
            {
                foreach (var testCase in testCases)
                {
                    testCase.Action?.Execute(control);
                }
            }
        }
    }
}
