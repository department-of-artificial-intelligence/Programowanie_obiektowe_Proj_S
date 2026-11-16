using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Interfaces
{
    public interface IReportService
    {
        void GenerateMonthlyReport(List<Account> accounts);
        void GenerateExpenseChart(List<Account> accounts);
    }
}
