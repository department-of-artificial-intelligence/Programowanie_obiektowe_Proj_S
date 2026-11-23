using DomowyBudzet.Model.DataModels;

namespace DomowyBudzet.Service.Abstactions;

public interface IBudgetStatistics {
        decimal AvgByCategory(CategoryType categoryType, TransactionType? transactionType = null);
        decimal AvgByType(TransactionType type);
        decimal MaxByCategory(CategoryType categoryType, TransactionType? transactionType = null);
        decimal MaxByType(TransactionType type);
        decimal MinByCategory(CategoryType categoryType, TransactionType? transactionType = null);
        decimal MinByType(TransactionType type);
        decimal SumByCategory(CategoryType categoryType, TransactionType? transactionType = null);
        decimal SumByType(TransactionType type);
}