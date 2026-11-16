using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model.DataModels;

public class Branch
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public Network Network { get; set; } = null!;
    public CashRegister CashRegister { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();

}
