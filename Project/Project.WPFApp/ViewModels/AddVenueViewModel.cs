using Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.WPFApp.ViewModels
{
    public class AddVenueViewModel : INotifyPropertyChanged
    {
        private string _nazwa;
        public string Nazwa
        {
            get => _nazwa;
            set { _nazwa = value; OnPropertyChanged(nameof(_nazwa)); }
        }
        private string _miasto;
        public string Miasto
        {
            get => _miasto;
            set { _miasto = value; OnPropertyChanged(nameof(_miasto)); }
        }
        private string _pojemnosc;
        public string Pojemnosc
        {
            get => _pojemnosc;
            set { _pojemnosc = value; OnPropertyChanged(nameof(_pojemnosc)); }
        }
        private Window _okno;
        public Venue NowyLokal { get; set; }

        public ICommand DodajCommand { get; }

        public AddVenueViewModel(Window okno)
        {
            DodajCommand = new Komenda(Dodaj);

            _okno = okno;
        }

        private void Dodaj(object obj)
        {
            if(int.TryParse(Pojemnosc, out int pojemnoscint) == false)
            {
                MessageBox.Show("Niepoprawna pojemność");
            }
            else 
            {
                NowyLokal = new Venue()
                {
                    Name = Nazwa,
                    City = Miasto,
                    Capacity = pojemnoscint
                };
            }
            

            if (_okno != null)
            {
                _okno.DialogResult = true;
                _okno.Close();
            }

        }


        // powiadamianie o zmianie właściwości
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
