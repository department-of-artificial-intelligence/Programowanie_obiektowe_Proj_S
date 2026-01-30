using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project.DAL;
using Project.Model;


namespace Project.WPFApp.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private readonly ApplicationDBContext _db;

        public ObservableCollection<Artist> Artyści { get; set; }
        public ObservableCollection<Venue> Lokale { get; set; }
        public ObservableCollection<Event> Wydarzenia {get; set; }
        public ObservableCollection<Concert> Koncerty { get; set; }
        public ObservableCollection<Person> Osoby { get; set; }
        public ObservableCollection<Ticket> Bilety { get; set; }

        private Artist _wybranyArtysta;
        public Artist WybranyArtysta
        {
            get => _wybranyArtysta;
            set {  _wybranyArtysta = value; OnPropertyChanged(nameof(WybranyArtysta)); }
        }
        private Venue _wybranyLokal;
        public Venue WybranyLokal
        {
            get => _wybranyLokal;
            set { _wybranyLokal = value; OnPropertyChanged(nameof(WybranyLokal)); }
        }
        private Concert _wybranyKoncert;
        public Concert WybranyKoncert
        {
            get => _wybranyKoncert;
            set { _wybranyKoncert = value; OnPropertyChanged(nameof(WybranyKoncert)); }
        }
        private int _indeksWybranejZakładki;
        public int IndeksWybranejZakładki
        {
            get => _indeksWybranejZakładki;
            set { _indeksWybranejZakładki = value; OnPropertyChanged(nameof(IndeksWybranejZakładki)); }
        }

        public ICommand UsunElementCommand { get; }
        public ICommand DodajElementCommand { get; }

        public MainViewModel()
        {
            _db = new ApplicationDBContext();
            _db.Database.EnsureCreated();

            Artyści = new ObservableCollection<Artist>(_db.Artists.ToList());
            Lokale = new ObservableCollection<Venue>(_db.Venues.ToList());
            Wydarzenia = new ObservableCollection<Event>(_db.Events.ToList());
            Koncerty = new ObservableCollection<Concert>(_db.Concerts.ToList());

            UsunElementCommand = new Komenda(UsunElement);
            DodajElementCommand = new Komenda(DodajElement);
        }

        private void UsunElement(object obj)
        {
            switch (IndeksWybranejZakładki)
            {
                case 0:
                    if (WybranyArtysta != null)
                    {
                        _db.Artists.Remove(WybranyArtysta);
                        _db.SaveChanges();
                        Artyści.Remove(WybranyArtysta);
                        WybranyArtysta = null;
                        MessageBox.Show("Usunięto artystę");
                    }
                    else MessageBox.Show("Wybierz artystę do usunięcia");
                    break;
                case 1:
                    if (WybranyLokal != null)
                    {
                        _db.Venues.Remove(WybranyLokal);
                        _db.SaveChanges();
                        Lokale.Remove(WybranyLokal);
                        WybranyLokal = null;
                        MessageBox.Show("Usunięto lokal");
                    }
                    else MessageBox.Show("Wybierz lokal do usunięcia");
                    break;
                case 2:
                    if (WybranyKoncert != null)
                    {
                        _db.Concerts.Remove(WybranyKoncert);
                        _db.SaveChanges();
                        Koncerty.Remove(WybranyKoncert);
                        WybranyKoncert = null;
                        MessageBox.Show("Usunięto koncert");
                    }
                    else MessageBox.Show("Wybierz koncert do usunięcia");
                    break;
            }
        }

        private void DodajElement(object obj)
        {
            switch (IndeksWybranejZakładki)
            {
                case 0: // okno dodania artysty
                    
                    var okno0 = new AddArtistWindow();
                    var vmAddArtist = new AddArtistViewModel(okno0);
                    okno0.DataContext = vmAddArtist;
                    
                    if (okno0.ShowDialog() == true)
                    {
                        var nowyArtysta = vmAddArtist.NowyArtysta;

                        _db.Artists.Add(nowyArtysta);
                        _db.SaveChanges();
                        Artyści.Add(nowyArtysta);
                    }

                    break;
                case 1: // okno dodania lokalu
                    var okno1 = new AddVenueWindow();
                    var vmAddVenue = new AddVenueViewModel(okno1);
                    okno1.DataContext = vmAddVenue;

                    if (okno1.ShowDialog()== true)
                    {
                        var nowyLokal = vmAddVenue.NowyLokal;
                        
                        _db.Venues.Add(nowyLokal);
                        _db.SaveChanges();
                        Lokale.Add(nowyLokal);
                    }
                    break;
                case 2: // okno dodania koncertu
                    var okno2 = new AddConcertWindow();
                    var vmAddConcert = new AddConcertViewModel(okno2, Artyści, Lokale);
                    okno2.DataContext = vmAddConcert;

                    if (okno2.ShowDialog()== true)
                    {
                        var nowyKoncert = vmAddConcert.NowyKoncert;

                        _db.Concerts.Add(nowyKoncert);
                        _db.SaveChanges();
                        Koncerty.Add(nowyKoncert);
                    }
                    break;
            }
        }

        // powiadamianie o zmianie właściwości
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
