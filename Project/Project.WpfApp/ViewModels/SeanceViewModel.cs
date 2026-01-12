using Project.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class SeanceViewModel
{
    public Seance Seance { get; set; } = new();
    public ObservableCollection<Film> AvailableFilms { get; set; } = [];
    public ObservableCollection<Auditorium> AvailableAuditoriums { get; set; } = [];

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public SeanceViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (Seance.StartTime <= DateTime.Now || Seance.Price <= 0)
            {
                MessageBox.Show("Start time must be in future and price must be positive.");
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