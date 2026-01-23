using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using WypozyczalniaSamochodow.Common;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.View.Abstractions;

namespace WypozyczalniaSamochodow.ViewModel
{
    public class MainViewModel
    {
        public List<Branch> Branches { get; set; }
        private readonly IMainWindow _mainWindow;
        private readonly IServiceProvider _serviceProvider;

        public ICommand LoadCommand { get; set; }
        public ICommand ShowAddBranchWindowCommand { get; set; }
        //public ICommand EditBranchCommand { get; set; }
        //public ICommand DeleteBranchCommand { get; set; }
        
        public MainViewModel(IMainWindow mainWindow, IServiceProvider serviceProvider)
        {
            _mainWindow = mainWindow;
            _serviceProvider = serviceProvider;
            Branches = new List<Branch>();
            Branches.AddRange(new List<Branch>()
            {
                new Branch() { Name = "Oddział A", City="Częstochowa", Address="Dąbrowskiego 1", ContactNumber="111222333" },
                new Branch() { Name = "Oddział B", City="Katowice", Address="Katowicka 12", ContactNumber="444555666" }
            });
            LoadCommand = new RelayCommand(LoadCommand_Loaded);
            ShowAddBranchWindowCommand = new RelayCommand(ShowAddBranchWindow_Click);
            //EditBranchCommand = new RelayCommand(EditBranchWindow_Click);
            //DeleteBranchCommand = new RelayCommand(DeleteBranch_Click); 
        }

        private void LoadCommand_Loaded(object obj) { }

        private void ShowAddBranchWindow_Click(object sender) 
        {
            var addBranchWindow = _serviceProvider.GetRequiredService<IAddBranchWindow>();
            addBranchWindow.DataContext = new AddBranchViewModel(addBranchWindow, _serviceProvider);

            if(addBranchWindow.ShowDialog() == true)
            {
                Branches.Add(addBranchWindow.Branch);
                _mainWindow.DataGridBranches.Items.Refresh();
            }
        }

        //private void EditBranchWindow_Click(object sender) { }

        //private void DeleteBranch_Click(object sender) { }
    }
}
