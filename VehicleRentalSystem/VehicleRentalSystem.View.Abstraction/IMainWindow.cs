using VehicleRentalSystem.Common;
using System.Windows.Controls;

namespace VehicleRentalSystem.View.Abstractions
{
    public interface IMainWindow : IWindow
    {
        public DataGrid DataGridVehicles { get; set; }
        public DataGrid DataGridDepartments { get; set; }
    }

}
