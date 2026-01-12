using Project.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class FilmViewModel
{
    public Film Film { get; set; } = new();
    public ObservableCollection<Actor> AvailableActors { get; set; } = [];

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public FilmViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (string.IsNullOrWhiteSpace(Film.Title) || Film.DurationMinutes == 0)
            {
                MessageBox.Show("Title and duration are required.");
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