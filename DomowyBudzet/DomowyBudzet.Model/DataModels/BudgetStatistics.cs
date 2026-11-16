namespace DomowyBudzet.Model.DataModels;
public class BudgetStatistics {
    private IEnumerable<Transaction> Transactions { get; set; }

    public BudgetStatistics() {
        Transactions = [];
    }
    public BudgetStatistics(IEnumerable<Transaction> transactions) {
        ArgumentNullException.ThrowIfNull(transactions, "List of transactions has to be initialized (can't be null)");

        Transactions = transactions;
    }
    public BudgetStatistics(BudgetManager budgetManager) {
        ArgumentNullException.ThrowIfNull(budgetManager, "BudgetManager has to be initialized (can't be null)");

        Transactions = budgetManager.Transactions;
    }

    //public decimal AVG()
    //{
    //    return Transactions.Average()
    //}
}
