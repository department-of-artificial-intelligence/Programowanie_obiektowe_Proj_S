using Project.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class ReservationViewModel
{
    public Reservation Reservation { get; set; } = new();
    public ObservableCollection<Seance> AvailableSeances { get; set; } = [];

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public ReservationViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    public string[] PaymentMethods { get; } = Enum.GetNames(typeof(Models.PaymentMethod));

    public string SelectedSeat { get; set; } = string.Empty;
    public ObservableCollection<string> AvailableSeats { get; set; } = [];
    public decimal TotalAmount { get; set; } = 0;


    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (string.IsNullOrWhiteSpace(Reservation.CustomerFirstName) || string.IsNullOrWhiteSpace(Reservation.CustomerLastName))
            {
                MessageBox.Show("Customer name is required.");
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