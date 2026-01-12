using Project.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class TicketViewModel
{
    public Ticket Ticket { get; set; } = new();
    public ObservableCollection<Reservation> AvailableReservations { get; set; } =[];
    public ObservableCollection<Cinema> AvailableCinemas { get; set; } = [];
    public ObservableCollection<Auditorium> AvailableAuditoriums { get; set; } = [];
    public ObservableCollection<Seance> AvailableSeances { get; set; } = [];
    public ObservableCollection<Film> AvailableFilms { get; set; } = [];

    public string[] AvailableTicketTypes { get; } = Enum.GetNames(typeof(TicketType));

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public TicketViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (string.IsNullOrWhiteSpace(Ticket.SeatId) || Ticket.OriginalPrice <= 0)
            {
                MessageBox.Show("Seat ID and price are required.");
                return;
            }

            w.DialogResult = true;
            w.Close();
        }
    }

    private void Close(object? parametr)
    {
        if (parametr is Window w)
        {
            w.DialogResult = false;
            w.Close();
        }
    }
}