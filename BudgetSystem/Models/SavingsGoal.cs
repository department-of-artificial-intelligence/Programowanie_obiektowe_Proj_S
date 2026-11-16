using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SavingsGoal
    {
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
    }
}
