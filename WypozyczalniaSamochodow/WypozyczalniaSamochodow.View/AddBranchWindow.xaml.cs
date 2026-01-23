using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.View.Abstractions;

namespace WypozyczalniaSamochodow.View
{
    /// <summary>
    /// Interaction logic for AddBranchWindow.xaml
    /// </summary>
    public partial class AddBranchWindow : Window, IAddBranchWindow
    {
        public Branch Branch { get; set; }
        public AddBranchWindow()
        {
            InitializeComponent();
        }
    }
}
