using Calculator.Application;
using System.ComponentModel;
using System.Windows.Input;
using AppCalculator = Calculator.Application.Calculator;

namespace Calculator.UI.WpfApp.ViewModels;
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public ICommand ResetCommand => new RelayCommand(p => Reset());
        public ICommand IncludeSignCommand => new RelayCommand(p => IncludeSign(p.ToString()));
        public ICommand SetOperationCommand => new RelayCommand(p => SetOperation(p.ToString()));
        public ICommand ChangeSignCommand => new RelayCommand(p => ChangeSign());
        public ICommand CalculateResultCommand => new RelayCommand(p => CalculateResult());


        private double _arg1, _arg2;
        private IOperation _operation;

        private string _display = "0";
        public string Display
        {
            get => _display;
            set
            {
                _display = value;
                OnPropertyChanged(nameof(Display));
            }
        }

        private void Reset()
        {
            Display = "0";
            _arg1 = _arg2 = 0;
            _display = null;
        }

        private void IncludeSign(string sign)
        {
        if (sign == "," && Display.Contains(",")) return;
        if(Display == "0" && sign!= ",")
            Display = sign;
            else Display += sign;
        }

        private void SetOperation(string operationSymbol)
        {
            if (double.TryParse(Display, out double arg))
            {
                _arg1 = arg;
                _operation = OperationFactory.GetOperation(operationSymbol);
                Display = "0";
            }
        }

        private void CalculateResult()
        {
            if (_operation is null) return;
            if (double.TryParse(Display, out double arg))
            {
                _arg2 = arg;
                double result = AppCalculator.Calculate(_operation, _arg1, _arg2);
                Display = result.ToString();
            }
        }

        private void ChangeSign()
        {
            if (double.TryParse(Display, out var number))
                Display = (-number).ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }