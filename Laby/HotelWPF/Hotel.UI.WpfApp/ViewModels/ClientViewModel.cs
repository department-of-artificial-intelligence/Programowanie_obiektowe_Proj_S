using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Hotel.Model;

namespace Hotel.UI.WpfApp.ViewModels;

partial class ClientViewModel
{
    public Client? Client { get; set; }

    public ICommand SubmitCommand { get; set; }
    public ICommand CloseCommand { get; set; }


    public ClientViewModel()
    {
        SubmitCommand = new RelayCommand(Submit);
        CloseCommand = new RelayCommand(Close);
    }


    private void Submit(object? parameter)
    {
        if(
            parameter is Window w &&
            Client is not null)
        {
            // wpisać wzorzec nazwy
            var namePattern = @"^";
            if (
                //Napisać właściwości dla firstName, lastName itd.
                !Regex.IsMatch(Client.FirstName, namePattern)
                )
            {
                MessageBox.Show("Wprowadzone dane są niepoprawne.");
                return;
            }

            w.DialogResult = true;
            w.Close();
        }
    }

    private void Close(object? parameter)
    {
        if(parameter is Window w)
        {
            w.DialogResult = false;
            w.Close();
        }
    }
}
