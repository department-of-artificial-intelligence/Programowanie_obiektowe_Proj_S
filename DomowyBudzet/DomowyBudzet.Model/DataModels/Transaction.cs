namespace DomowyBudzet.Model.DataModels;

public class Transaction {
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public Category Category { get; set; }
    public TransactionType Type { get; set; }

    public Transaction() {
        Id = 0;
        Amount = 0m;
        Category = new Category();
        Type = TransactionType.Expense;
    }
    public Transaction(int id, decimal amount, Category category, TransactionType type) {
        if(id < 0)
            throw new ArgumentException("ID can't be less than 0");
        if(amount < 0)
            throw new ArgumentException("Amount of money can't be less than 0");
        ArgumentNullException.ThrowIfNull(category, "Category has to be initialized (can't be null)");

        Id = id;
        Amount = amount;
        Category = category;
        Type = type;
    }

    public override string ToString() {
        return $"ID: {Id}, Amount: {Amount}, Category: {Category}, Type: {Type}";
    }
}
