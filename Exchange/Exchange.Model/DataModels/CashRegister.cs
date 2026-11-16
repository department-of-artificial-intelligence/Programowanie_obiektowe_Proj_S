using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model.DataModels;

public class CashRegister
{
    public int Id { get; set; }
    public List<CashBalance> Balances { get; set; } = new();
}
