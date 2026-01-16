using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WypozyczalniaSamochodow.View.Abstractions;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Common;

namespace WypozyczalniaSamochodow.ViewModel;

public class AddBranchViewModel
{
    private readonly IServiceProvider _serviceProvider;

    public ICommand AddBranchCommand { get; set; }
    public IAddBranchWindow AddBranchWindow { get; }

    public AddBranchViewModel(IAddBranchWindow addBranchWindow, IServiceProvider serviceProvider)
    {
        AddBranchWindow = addBranchWindow;
        AddBranchWindow.Branch = new Branch();
        _serviceProvider = serviceProvider;
        AddBranchCommand = new RelayCommand(AddBranch_Click);
    }

    private void AddBranch_Click(object sender)
    {
        AddBranchWindow.DialogResult = true;
    }
}


