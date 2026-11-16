using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model.DataModels;

public class ExchangeRate
{
    public int Id { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SellRate { get; set; }
    public DateTime EffectiveDate { get; set; }
    public bool IsActive { get; set; }
    public Currency Currency { get; set; } = null!;
}
