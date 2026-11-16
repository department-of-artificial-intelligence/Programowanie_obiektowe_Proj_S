using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Model.DataModels;
using Exchange.Model.Enums;

namespace Exchange.Services;

public class TransactionService : ITransactionService
{
    public const string BaseCurrencyCode = "PLN";

    private List<Branch> _branches;
    private List<ExchangeRate> _rates;
    private List<Customer> _customers;
    private List<Transaction> _transactions = new();

    public TransactionService(List<Branch> branches, List<ExchangeRate> rates, List<Customer> customers)
    {
        _branches = branches;
        _rates = rates;
        _customers = customers;
    }

    public Transaction BuyCurrency(int branchId, int employeeId, string currencyCode, decimal amount, int? customerId)
    {
        if (amount <= 0)
            throw new ArgumentException("Kwota musi być większa od zera.");

        var branch = branchId;
        var employee = employeeId;
        var rate = 2;  //
        decimal totalPLN = amount * rate;
        return new();

    }

    public Transaction SellCurrency(int branchId, int employeeId, string currencyCode, decimal amount, int? customerId)
    {
        return new();
    }

    public Transaction ExchangeCrossCurrency(int branchId, int employeeId, string fromCurrency, string toCurrency, decimal amountFrom, int? CustomerId)
    {
        return new();
    }
}
