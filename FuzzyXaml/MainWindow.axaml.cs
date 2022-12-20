using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace FuzzyXaml;

public abstract class TestAction
{
}

public class ButtonExecuteCommandTestAction : TestAction
{
    
}

public class TextBoxSetTextPropertyTestAction : TestAction
{
    
}

public class TesCase
{
    public int Level { get; set; }

    public int Index { get; set; }
    
    public TestAction Action { get; set; }
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
            int level = 0;
            
            var controls = this.GetVisualDescendants().OfType<Control>().ToList();
            for (var index = 0; index < controls.Count; index++)
            {
                var control = controls[index];
                // Console.WriteLine($"{control}");

                // TODO: use tab order
                // TODO: generate paths for testing
                // TODO: use random order

                switch (control)
                {
                    case TextBox textBox:
                    {
                        Console.WriteLine($"[{level}:{index}] Set {nameof(TextBox)}.Text");
                        textBox.Text = "Random Text";
                        // TODO: record TextBoxSetTextPropertyTestAction
                        // TODO: after recording action check level ? record next action ? check tree diff ?
                        break;
                    }
                    case Button button:
                    {
                        if (button.IsEnabled && button.Command is { })
                        {
                            Console.WriteLine($"[{level}:{index}] Execute {nameof(Button)}.Command");
                            button.Command.Execute(null);
                            // TODO: record ButtonExecuteCommandTestAction
                            // TODO: after recording action check level ? record next action ? check tree diff ?
                        }

                        break;
                    }
                }
            }
        }
    }

    public void SomeMethod()
    {

    }
}
