using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICurrencyService
    {
        decimal ConvertCurrency(decimal amount, string sourceCurrency, string targetCurrency);
    }
}
