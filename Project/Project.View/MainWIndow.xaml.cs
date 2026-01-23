using Project.View.Abstractions;
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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for MainWIndow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public DataGrid DataGridDrivers
        {
            get => DataGridDrivers;
            set => DataGridDrivers = value;
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
