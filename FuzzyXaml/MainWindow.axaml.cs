using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace FuzzyXaml;

public abstract class TestAction
{
    public abstract void Execute(object control);
}

public class ButtonExecuteCommandTestAction : TestAction
{
    public object? Parameter { get; set; }

    public ButtonExecuteCommandTestAction(object? parameter)
    {
        Parameter = parameter;
    }

    public override void Execute(object control)
    {
        Console.WriteLine($"[{nameof(Button)}.{nameof(Button.Command)}]");

        if (control is Button button && button.IsEnabled && button.Command is { })
        {
           button.Command.Execute(Parameter);
        }  
    }
}

public class TextBoxSetTextPropertyTestAction : TestAction
{
    public string? Text { get; set; }

    public TextBoxSetTextPropertyTestAction(string? text)
    {
        Text = text;
    }

    public override void Execute(object control)
    {
        Console.WriteLine($"[{nameof(TextBox)}.{nameof(TextBox.Text)}]");

        if (control is TextBox textBox && textBox.IsEnabled)
        {
            textBox.Text = Text;
        }  
    }
}

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

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.AddHandler(InputElement.KeyDownEvent, OnKeyDown);
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.F5)
        {
            var testCases = GenerateTestCases();
            if (testCases.Count > 0)
            {
                ExecuteTestCases(testCases);
            }
        }
    }

    private void ExecuteTestCases(List<TestCase> testCases)
    {
        var level = 0;
        var controls = this.GetVisualDescendants().OfType<Control>().ToList();

        for (var index = 0; index < controls.Count; index++)
        {
            var control = controls[index];

            // Console.WriteLine($"[{level}:{index}] {control.GetType().FullName}");

            foreach (var testCase in testCases)
            {
                if (testCase.Level == level && testCase.Index == index)
                {
                    testCase.Action?.Execute(control);
                }
            }
        }
    }

    private List<TestCase> GenerateTestCases()
    {
        var testCases = new List<TestCase>();
        var level = 0;
        var controls = this.GetVisualDescendants().OfType<Control>().ToList();

        for (var index = 0; index < controls.Count; index++)
        {
            var control = controls[index];

            // Console.WriteLine($"[{level}:{index}] {control.GetType().FullName}");

            switch (control)
            {
                case TextBox:
                {
                    // Console.WriteLine($"[{nameof(TextBox)}.{nameof(TextBox.Text)}]");
                    testCases.Add(new TestCase(level, index, new TextBoxSetTextPropertyTestAction("Random Text")));
                    break;
                }
                case Button:
                {
                    // Console.WriteLine($"[{nameof(Button)}.{nameof(Button.Command)}]");
                    testCases.Add(new TestCase(level, index, new ButtonExecuteCommandTestAction(null)));
                    break;
                }
            }
        }

        return testCases;
    }

    public void SomeMethod()
    {

    }
}
