using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Database;
using Hotel.UI.WpfApp.Views;

namespace Hotel.UI.WpfApp.ViewModels;
public class MainViewModel : INotifyPropertyChanged
{
    private readonly Db _db;

    public Client? SelectedClient { get; set; }
    public Room? SelectedRoom { get; set; }
    public Reservation? SelectedReservation { get; set; }


    public ObservableCollection<Client> Clients { get; set; }
    public ObservableCollection<Room> Rooms { get; set; }
    public ObservableCollection<Reservation> Reservations { get; set; }

    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set
        {
            if (_selectedTabIndex != value)
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
            }
        }
    }


    // ====================== Client commands ======================
    public ICommand AddClientCommand { get; }
    public ICommand EditClientCommand { get; }
    public ICommand DeleteClientCommand { get; }
    public ICommand PrintClientsCommand { get; }

    // ====================== Room commands ======================
    public ICommand AddRoomCommand { get; }
    public ICommand EditRoomCommand { get; }
    public ICommand DeleteRoomCommand { get; }
    public ICommand PrintRoomsCommand { get; }

    // ====================== Reservation commands ======================
    public ICommand AddReservationCommand { get; }
    public ICommand EditReservationCommand { get; }
    public ICommand DeleteReservationCommand { get; }
    public ICommand PrintReservationsCommand { get; }



    public MainViewModel()
    {
        _db = new Db();

        Clients = new(_db.Clients);
        Rooms = new(_db.Rooms);
        Reservations = new(_db.Reservations);

        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand(EditClient);
        DeleteClientCommand = new RelayCommand(DeleteClient);
        PrintClientsCommand = new RelayCommand(PrintClients);
    }

    private void AddClient(object? parameter)
    {
        // Brak inicjalizacji IndeksWybranejZakładki
        SelectedTabIndex = 0;
        SelectedClient = new();
        var window = new ClientWindow()
        {
            DataContext = new ClientViewModel()
            {
                Client = SelectedClient
            }
        };

        if (window.ShowDialog() == true)
        {
            _db.Clients.Add(SelectedClient);
            _db.SaveChanges();

            Clients.Add(SelectedClient);
        }
    }

    private void EditClient(object? parameter)
    {
        SelectedTabIndex = 0;
        if (SelectedClient is null) return;
        //Błąd: There is no argument given that corresponds to the required parameter 'lastName' of 'Client.Client(string, string, string)'
        //var clientCopy = new Client(SelectedClient.FirstName);
        var clientIndex = Clients.IndexOf(SelectedClient);

        var window = new ClientWindow()
        {
            DataContext = new ClientViewModel()
            {
                Client = SelectedClient
            }
        };

        if (window.ShowDialog() == true)
        {
            _db.SaveChanges();
            if (clientIndex >= 0)
                Clients[clientIndex] = SelectedClient;
        }
        else
        {
            //SelectedClient.FirstName = clientCopy.FirstName;
            //itd.

            /*if (clientIndex >= 0)
                Clients[clientIndex] = clientCopy;*/
        }
    }

    private void DeleteClient(object? parameter)
    {
        SelectedTabIndex = 0;

        if (SelectedClient is null) return;
        _db.Clients.Remove(SelectedClient);
        _db.SaveChanges();

        Clients.Remove(SelectedClient);
    }

    private void PrintClients(object? parameter)
    {
        Clients.Clear();
        foreach (var client in _db.Clients) Clients.Add(client);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}
