using DomowyBudzet.Model.DataModels;

namespace DomowyBudzet.Service.Abstractions;

public interface IBudgetManager {
        decimal Balance { get; }
        IEnumerable<Transaction> Transactions { get; }

        void AddTransaction(Transaction transaction);
        string GetBalance();
        string GetTransactionHistory();
}