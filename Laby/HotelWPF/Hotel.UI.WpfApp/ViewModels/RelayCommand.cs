using System.Windows.Input;

namespace Hotel.UI.WpfApp.ViewModels
{
    public class RelayCommand(Action<object?> action) : ICommand
    {
        private readonly Action<object?> _action = action;

        public bool CanExecute(object? _) => true;
        public void Execute(object? parameter) => _action(parameter);

        public event EventHandler? CanExecuteChanged { add { } remove { } }
    }
}
