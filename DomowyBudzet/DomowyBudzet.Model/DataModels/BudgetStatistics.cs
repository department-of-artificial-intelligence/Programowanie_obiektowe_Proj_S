namespace DomowyBudzet.Model.DataModels;
public class BudgetStatistics {
    private List<Transaction> Transactions { get; set; }

    public BudgetStatistics() {
        Transactions = [];
    }
    public BudgetStatistics(IEnumerable<Transaction> transactions) {
        ArgumentNullException.ThrowIfNull(transactions, "List of transactions has to be initialized (can't be null)");

        Transactions = transactions.ToList();
    }
    public BudgetStatistics(BudgetManager budgetManager) {
        ArgumentNullException.ThrowIfNull(budgetManager, "BudgetManager has to be initialized (can't be null)");

        Transactions = budgetManager.Transactions.ToList();
    }

    //public decimal AVG()
    //{
    //    return Transactions.Average()
    //}
}
