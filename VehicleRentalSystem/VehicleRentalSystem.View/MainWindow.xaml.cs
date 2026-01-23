using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VehicleRentalSystem.View.Abstractions;

namespace VehicleRentalSystem.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public DataGrid DataGridVehicles
        {
            get => dataGridVehicles;
            set => dataGridVehicles = value;
        }

        public DataGrid DataGridDepartments
        {
            get => dataGridDepartments;
            set => dataGridDepartments = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
