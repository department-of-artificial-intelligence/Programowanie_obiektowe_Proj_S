using System;
using System.Windows.Input;

namespace Calculator.UI.WpfApp.ViewModels
{
    public class RelayCommand(Action<object> commandAction) : ICommand
    {
        private readonly Action<object> _commandAction = commandAction;
        public bool CanExecute(object _) => true;
        public void Execute(object _) => _commandAction(_);
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
