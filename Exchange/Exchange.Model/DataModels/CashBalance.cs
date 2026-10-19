using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model.DataModels
{
    public class CashBalance
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public int CashRegisterId { get; set; }

    }
}
