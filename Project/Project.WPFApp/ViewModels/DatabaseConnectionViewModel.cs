using Project.WPFApp.Common;

namespace Project.WPFApp.ViewModels
{
    public class DatabaseConnectionViewModel : BaseViewModel
    {
        private string _serverName = string.Empty;
        private string _databaseName = string.Empty;
        private string _parameters = string.Empty;

        public WpfServices Services { get; init; } = new WpfServices();
        
        public bool IsCanBeConnected { get; private set; } = true;
        
        public string ConnectionString { get; set; } = string.Empty;

        public string ServerName
        {
            get => _serverName;
            set
            {
                this._serverName = value;
                this.UpdateConnectionString();
            }
        }

        public string DatabaseName
        {
            get => _databaseName;
            set
            {
                this._databaseName = value;
                this.UpdateConnectionString();
            }
        }

        public string Parameters
        {
            get => _parameters;
            set
            {
                this._parameters = value;
                this.UpdateConnectionString();
            }
        }

        public RelayCommand<object> ConnectCommand { get; set; }
        
        public DatabaseConnectionViewModel()
        {
            this.ConnectCommand = RelayCommandFactory.Create(this.Connect);
        }
        
        private void UpdateConnectionString()
        {
            ConnectionString = $"Server={ServerName};Database={DatabaseName};{Parameters}";
            this.OnPropertyChanged(nameof(ConnectionString));
        }

        private async Task Connect()
        {
            this.IsCanBeConnected = false;
            
            await this.Services.OpenDatabaseConnectionAsync(this.ConnectionString);
            
            this.CloseRequest();
        }
    }
}
