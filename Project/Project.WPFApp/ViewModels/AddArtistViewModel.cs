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
    public class AddArtistViewModel: INotifyPropertyChanged
    {
        private string _nazwa;
        public string Nazwa
        {
            get => _nazwa;
            set { _nazwa = value; OnPropertyChanged(nameof(_nazwa)); }
        }
        private string _gatunek;
        public string Gatunek
        {
            get => _gatunek;
            set { _gatunek = value; OnPropertyChanged(nameof(_gatunek)); }
        }
        private string _kraj;
        public string Kraj
        {
            get => _kraj;
            set { _kraj = value; OnPropertyChanged(nameof(_kraj)); }
        }
        private Window _okno;
        public Artist NowyArtysta { get; set; }

        public ICommand DodajCommand { get; }

        public AddArtistViewModel(Window okno)
        {
            DodajCommand = new Komenda(Dodaj);
            
            _okno = okno;
        }

        private void Dodaj(object obj)
        {
            NowyArtysta = new Artist()
            {
                Name = Nazwa,
                Genre = Gatunek,
                Country = Kraj
            };

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
