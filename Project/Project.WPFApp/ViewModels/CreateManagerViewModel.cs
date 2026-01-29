using System.Windows.Input;
using Project.WPFApp.Common;

namespace Project.WPFApp.ViewModels
{
    public class CreateManagerViewModel : BaseViewModel
    {
        public WpfServices Services { get; init; } = new WpfServices();
        
        public bool IsCanBeCreated { get; private set; } = true;
        
        public string ManagerFirstName { get; set; } = string.Empty;
        
        public string ManagerLastName { get; set; } = string.Empty;
        
        public DateTime ManagerBirthDate { get; set; } = DateTime.Now;
        
        public ICommand CreateManagerCommand { get; set; }
        
        public CreateManagerViewModel()
        {
            CreateManagerCommand = RelayCommandFactory.Create(this.CreateManager);
        }
        
        private async Task CreateManager()
        {
            this.IsCanBeCreated = false;
            
            await this.Services.ManagerService?.CreateManagerAsync(this.ManagerFirstName, this.ManagerLastName,
                this.ManagerBirthDate)!;
            
            this.CloseRequest();
        }
    }
}