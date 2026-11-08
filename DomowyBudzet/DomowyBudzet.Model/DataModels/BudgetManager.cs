namespace DomowyBudzet.Model.DataModels;

public class BudgetManager {
    public decimal Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public BudgetManager() {
        Balance = 0m;
        Transactions = [];
    }
    public BudgetManager(List<Transaction> transactions) {
        ArgumentNullException.ThrowIfNull(transactions, "List of transactions has to be initialized (can't be null)");

        Transactions = [.. transactions];
        Balance = transactions.Where(x => x.Type == TransactionType.Income).Sum(x => x.Amount) - transactions.Where(x => x.Type == TransactionType.Expense).Sum(x => x.Amount);
    }

    public void AddTransaction(Transaction transaction) {
        ArgumentNullException.ThrowIfNull(transaction, "Transaction has to be initialized (can't be null)");

        if(transaction.Type == TransactionType.Income) {
            Balance += transaction.Amount;
            Transactions.Add(transaction);
        }
        else {
            if(Balance - transaction.Amount < 0)
                throw new InvalidOperationException("You can't do this operation because you don't have enough money");
            else {
                Balance -= transaction.Amount;
                Transactions.Add(transaction);
            }
        }
    }

    public string GetBalance() { return $"{Balance:C}"; }
    public string GetTransactionHistory() { return "Transactions:\n" + string.Join("\n", Transactions); }

}
