using Project.Common;
using System.Windows.Controls;

namespace Project.View.Abstractions
{
    public interface IMainWindow : IWindow
    {
        public DataGrid DataGridDrivers { get; set; }
        // dopisa? pozosta?e klasy
    }

}
