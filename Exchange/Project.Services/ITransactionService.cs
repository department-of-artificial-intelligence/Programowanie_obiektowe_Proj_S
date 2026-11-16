using Exchange.Model.DataModels;

namespace Exchange.Services;

public interface ITransactionService
{
    Transaction BuyCurrency(int branchId, int employeeId, string currencyCode, decimal amount, int? customerId);

    Transaction SellCurrency(int branchId, int employeeId, string currencyCode, decimal amount, int? customerId);

    Transaction ExchangeCrossCurrency(int branchId, int employeeId, string fromCurrency, string toCurrency, decimal amountFrom, int? customerId);
}
