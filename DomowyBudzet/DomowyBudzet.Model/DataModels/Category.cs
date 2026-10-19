namespace DomowyBudzet.Model.DataModels;

public class Category {
    private string _name;
    List<Transaction> _amounts;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}
