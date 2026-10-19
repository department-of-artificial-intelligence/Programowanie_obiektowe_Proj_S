using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model.DataModels
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRateApplied { get; set; }
        public decimal TotalValueBaseCurrency { get; set; }
        public DateTime Date {  get; set; }
        public int BranchId { get; set; }
        public int EmployeeId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
