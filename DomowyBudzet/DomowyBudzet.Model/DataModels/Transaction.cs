namespace DomowyBudzet.Model.DataModels;

public class Transaction {
    private decimal _amount;
    private TransactionType _type;
    private Category _category;

    public decimal Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public TransactionType Type
    {
        get { return _type; }
    }
    public Category Category_
    {
        get { return _category; }
        set { _category = value; }
    }
}
