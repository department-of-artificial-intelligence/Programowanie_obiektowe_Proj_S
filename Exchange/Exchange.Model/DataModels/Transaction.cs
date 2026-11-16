using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exchange.Model.Enums;

namespace Exchange.Model.DataModels;

public class Transaction
{
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; } // 
    public decimal TargetAmount { get; set; }
    public decimal ExchangeRateApplied { get; set; }
    public decimal TotalValueBaseCurrency { get; set; }
    public DateTime Date {  get; set; }
    public string ReceiptNumber { get; set; } = string.Empty;
    public Branch Branch { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
    public Customer? Customer { get; set; }
    public Currency Currency { get; set; } = null!;
    public Currency? TargetCurrency { get; set; }
}
