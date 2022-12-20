using Avalonia.Controls;
using Avalonia.Input;

namespace FuzzyXaml;

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
            var testCases = this.GenerateTestCases();
            this.ExecuteTestCases(testCases);
        }
    }

    public void SomeMethod()
    {
        ClickMeButton.Content = "Clicked!";
    }
}
