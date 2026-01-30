using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.WPFApp.ViewModels
{
    public class Komenda(Action<object?> akcja): ICommand
    {
        private readonly Action<object?> _akcja = akcja;

        public bool CanExecute(object? _) => true;
        public void Execute(object? parametr) => _akcja(parametr);

        public event EventHandler? CanExecuteChanged { add { } remove { } }

    }
}
