using System;
using Avalonia.Controls;

namespace FuzzyXaml.Model.Actions;

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
