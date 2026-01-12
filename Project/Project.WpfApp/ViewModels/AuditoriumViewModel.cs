using Project.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class AuditoriumViewModel
{
    public Auditorium Auditorium { get; set; } = new();
    public ObservableCollection<Cinema> AvailableCinemas { get; set; } = [];

    public uint TotalCapacity => Auditorium.Rows * Auditorium.SeatsPerRow;

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public AuditoriumViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (string.IsNullOrWhiteSpace(Auditorium.Name) || Auditorium.RoomNumber == 0)
            {
                MessageBox.Show("Name and room number are required.");
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