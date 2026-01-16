using System.Windows.Input;

namespace Project.WPFApp.Common
{
    public class WpfCommandAdapter<T> : ICommand
    {
        private readonly Action<T?> _action;

        public bool Executable { get; set; }

        public event EventHandler? CanExecuteChanged;

        public WpfCommandAdapter(Action<T?> action, bool canExecute = true)
        {
            this._action = action;
            this.Executable = canExecute;
        }

        public bool CanExecute(object? _)
        {
            return this.Executable;
        }

        public void Execute(object? parameter)
        {
            this._action.Invoke((T?) parameter);
        }
    }

    static class WpfCommandAdapterFactory
    {
        public static WpfCommandAdapter<T> Create<T>(Action<T?> action)
        {
            return new WpfCommandAdapter<T>(action);
        }
    }
}
