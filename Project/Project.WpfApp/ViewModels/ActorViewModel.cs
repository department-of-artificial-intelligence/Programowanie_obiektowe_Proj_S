using Project.Models;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels;

public class ActorViewModel
{
    public Actor Actor { get; set; } = new();

    public ICommand ConfirmCommand { get; }
    public ICommand CloseCommand { get; }

    public ActorViewModel()
    {
        ConfirmCommand = new Command(Confirm);
        CloseCommand = new Command(Close);
    }

    private void Confirm(object? parametr)
    {
        if (parametr is Window w)
        {
            if (string.IsNullOrWhiteSpace(Actor.FirstName) || string.IsNullOrWhiteSpace(Actor.LastName))
            {
                MessageBox.Show("First name and last name are required.");
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