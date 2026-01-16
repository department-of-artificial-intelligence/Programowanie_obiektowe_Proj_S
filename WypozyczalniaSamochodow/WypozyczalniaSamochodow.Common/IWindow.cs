namespace WypozyczalniaSamochodow.Common;

public interface IWindow
{
    public object DataContext { get; set; }
    public bool? ShowDialog();
    public void Show();
    public bool? DialogResult { get; set; }
}