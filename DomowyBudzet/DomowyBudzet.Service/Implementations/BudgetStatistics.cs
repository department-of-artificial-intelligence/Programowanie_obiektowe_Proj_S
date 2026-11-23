using DomowyBudzet.Model.DataModels;
using DomowyBudzet.Service.Abstactions;

namespace DomowyBudzet.Service.Implementaions;

public class BudgetStatistics : IBudgetStatistics {
    private IEnumerable<Transaction> Transactions { get; set; }

    public BudgetStatistics()
    {
        Transactions = [];
    }
    public BudgetStatistics(IEnumerable<Transaction> transactions)
    {
        ArgumentNullException.ThrowIfNull(transactions, "List of transactions has to be initialized (can't be null)");

        Transactions = transactions;
    }
    public BudgetStatistics(BudgetManager budgetManager)
    {
        ArgumentNullException.ThrowIfNull(budgetManager, "BudgetManager has to be initialized (can't be null)");

        Transactions = budgetManager.Transactions;
    }

    public decimal SumByType(TransactionType type)
    {
        return Transactions.Where(x => x.Type == type).Sum(x => x.Amount);
    }
    public decimal AvgByType(TransactionType type)
    {
        return Transactions.Where(x => x.Type == type).Select(x => x.Amount).DefaultIfEmpty(0m).Average();
    }
    public decimal MaxByType(TransactionType type)
    {
        return Transactions.Where(x => x.Type == type).Select(x => x.Amount).DefaultIfEmpty(0m).Max();
    }
    public decimal MinByType(TransactionType type)
    {
        return Transactions.Where(x => x.Type == type).Select(x => x.Amount).DefaultIfEmpty(0m).Min();
    }
    public decimal SumByCategory(CategoryType categoryType, TransactionType? transactionType = null)
    {
        if (transactionType is not null)
            return Transactions.Where(x => x.Category.Type == categoryType && x.Type == transactionType).Sum(x => x.Amount);
        else
            return Transactions.Where(x => x.Category.Type == categoryType).Sum(x => x.Type == TransactionType.Income ? x.Amount : -x.Amount);
    }
    public decimal AvgByCategory(CategoryType categoryType, TransactionType? transactionType = null)
    {
        if (transactionType is not null)
            return Transactions.Where(x => x.Category.Type == categoryType && x.Type == transactionType).Select(x => x.Amount).DefaultIfEmpty(0m).Average();
        else
            return Transactions.Where(x => x.Category.Type == categoryType).Select(x => x.Type == TransactionType.Income ? x.Amount : -x.Amount).DefaultIfEmpty(0m).Average();
    }
    public decimal MaxByCategory(CategoryType categoryType, TransactionType? transactionType = null)
    {
        if (transactionType is not null)
            return Transactions.Where(x => x.Category.Type == categoryType && x.Type == transactionType).Select(x => x.Amount).DefaultIfEmpty(0m).Max();
        else
            return Transactions.Where(x => x.Category.Type == categoryType).Select(x => x.Type == TransactionType.Income ? x.Amount : -x.Amount).DefaultIfEmpty(0m).Max();
    }
    public decimal MinByCategory(CategoryType categoryType, TransactionType? transactionType = null)
    {
        if (transactionType is not null)
            return Transactions.Where(x => x.Category.Type == categoryType && x.Type == transactionType).Select(x => x.Amount).DefaultIfEmpty(0m).Min();
        else
            return Transactions.Where(x => x.Category.Type == categoryType).Select(x => x.Type == TransactionType.Income ? x.Amount : -x.Amount).DefaultIfEmpty(0m).Min();
    }
}
