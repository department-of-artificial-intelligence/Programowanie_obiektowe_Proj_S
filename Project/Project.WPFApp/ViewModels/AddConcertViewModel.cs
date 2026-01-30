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
    public class AddConcertViewModel : INotifyPropertyChanged
    {
        public IList<Artist> DostepniArtysci { get; set; }
        public IList<Venue> DostepneLokale { get; set; }

        private Artist _wybranyArtysta;
        public Artist WybranyArtysta
        {
            get => _wybranyArtysta;
            set { _wybranyArtysta = value; OnPropertyChanged(nameof(_wybranyArtysta)); }
        }

        private Venue _wybranyLokal;
        public Venue WybranyLokal
        {
            get => _wybranyLokal;
            set { _wybranyLokal = value; OnPropertyChanged(nameof(_wybranyLokal)); }
        }

        private DateTime _data;
        public DateTime Data
        {
            get => _data;
            set { _data = value; OnPropertyChanged(nameof(_data)); }
        }
        private Window _okno;
        public Concert NowyKoncert { get; set; }

        public ICommand DodajCommand { get; }

        public AddConcertViewModel(Window okno, IList<Artist> artysci, IList<Venue> lokale)
        {
            DodajCommand = new Komenda(Dodaj);

            _okno = okno;

            DostepniArtysci = artysci;
            DostepneLokale = lokale;
            Data = DateTime.Now;
        }

        private void Dodaj(object obj)
        {
            NowyKoncert = new Concert()
            {
                Artist = WybranyArtysta,
                Venue = WybranyLokal,
                Date = Data
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
