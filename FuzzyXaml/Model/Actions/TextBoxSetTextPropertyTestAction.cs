using System;
using Avalonia.Controls;

namespace FuzzyXaml.Model.Actions;

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
