using WypozyczalniaSamochodow.Common;
using System.Windows.Controls;

namespace WypozyczalniaSamochodow.View.Abstractions;

public interface IMainWindow : IWindow
{
    public DataGrid DataGridBranches { get; set; }
}