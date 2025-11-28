using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
    public interface IReportGenerator
    {
        void GenerateSalesReport();
        void GenerateInventoryReport();
        void GenerateExpiryReport();
    }
}
