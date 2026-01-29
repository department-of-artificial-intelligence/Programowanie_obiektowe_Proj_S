using System.ComponentModel;

namespace Project.WPFApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public event Action? RequestClose;

        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected void CloseRequest()
        {
            this.RequestClose?.Invoke();
        }
    }
}
