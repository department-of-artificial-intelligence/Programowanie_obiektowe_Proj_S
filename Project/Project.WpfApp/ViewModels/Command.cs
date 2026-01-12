using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class Command(Action<object?> action) : ICommand
{
    private readonly Action<object?> _action = action;

    public bool CanExecute(object? _) => true;
    public void Execute(object? parametr) => _action(parametr);

    public event EventHandler? CanExecuteChanged { add { } remove { } }
}