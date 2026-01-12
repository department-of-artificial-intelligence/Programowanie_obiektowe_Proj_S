using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.Views
{
    public partial class InputDialog : Window
    {
        public string Answer { get; private set; } = string.Empty;

        public InputDialog(string question)
        {
            InitializeComponent();
            lblQuestion.Text = question;
            txtAnswer.Text = "";
            txtAnswer.Focus();
        }

        private void BtnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            Answer = txtAnswer.Text;
            DialogResult = true;
        }

        private void BtnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}