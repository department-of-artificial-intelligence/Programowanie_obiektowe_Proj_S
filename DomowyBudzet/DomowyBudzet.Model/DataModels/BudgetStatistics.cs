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

    public decimal SumByCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category, "Category has to be initialized (can't be null)");

        return Transactions.Where(x => x.Category.Type == category.Type).Sum(x => x.Amount);
    }
    public decimal AvgByCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category, "Category has to be initialized (can't be null)");

        return Transactions.Where(x => x.Category.Type == category.Type).Average(x => x.Amount);
    }
    public decimal MaxByCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category, "Category has to be initialized (can't be null)");

        return Transactions.Where(x => x.Category.Type == category.Type).Max(x => x.Amount);
    }
    public decimal MinByCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category, "Category has to be initialized (can't be null)");

        return Transactions.Where(x => x.Category.Type == category.Type).Min(x => x.Amount);
    }
}
