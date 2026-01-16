using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Hotel.Model;

namespace Hotel.UI.WpfApp.ViewModels;

partial class ReservationViewModel
{
    public Reservation? Reservation { get; set; }

    public ICommand SubmitCommand { get; set; }
    public ICommand CloseCommand { get; set; }


    public ReservationViewModel()
    {
        SubmitCommand = new RelayCommand(Submit);
        CloseCommand = new RelayCommand(Close);
    }


    private void Submit(object? parameter)
    {
        if (
            parameter is Window w &&
            Reservation is not null)
        {
            // Czy jest potrzebny wzorzec nazwy skoro nie ma string?
            /*var namePattern = @"^";
            if (
                //Napisać właściwości dla firstName, lastName itd.
                !Regex.IsMatch(Reservation., namePattern)
                )
            {
                MessageBox.Show("Wprowadzone dane są niepoprawne.");
                return;
            }*/

            w.DialogResult = true;
            w.Close();
        }
    }

    private void Close(object? parameter)
    {
        if (parameter is Window w)
        {
            w.DialogResult = false;
            w.Close();
        }
    }
}
