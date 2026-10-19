namespace DomowyBudzet.Model.DataModels;

public class User {
    private string _name;
    private decimal _balance;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    public decimal Balance {
        get { return _balance; }
        set { if(value > 0) _balance = value; }
    }

    public User(string name, decimal balance)
    {
        _name = name;
        _balance = balance;
    }
    public User(string name)
    {
        _name = name;
        _balance = 0;
    }

    public void AddTransaction(Transaction transaction)
    {
        //if(transaction.Type == )
    }
}
