using System.Windows.Input;

namespace Project.WPFApp.Common
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T?> _action;

        public bool Executable { get; set; }

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<T?> action, bool canExecute = true)
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

    static class RelayCommandFactory
    {
        public static RelayCommand<object> Create(Action action)
        {
            return new RelayCommand<object>(_ => action());
        }
        
        public static RelayCommand<T> Create<T>(Action<T?> action)
        {
            return new RelayCommand<T>(action);
        }
        
        public static RelayCommand<object> Create(Func<Task> action)
        {
            return new RelayCommand<object>(async void (_) =>
            {
                try
                {
                    await action();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}
