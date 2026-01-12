using Project.Models;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class CinemaViewModel
{
    public Cinema Cinema { get; set; } = new();

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public CinemaViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (string.IsNullOrWhiteSpace(Cinema.Name) || string.IsNullOrWhiteSpace(Cinema.Address))
            {
                MessageBox.Show("Name and address are required.");
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