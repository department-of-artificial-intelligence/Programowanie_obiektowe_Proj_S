using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model.DataModels;

public class Network
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NIP { get; set; } = string.Empty;
    public List<Branch> Branches { get; set; } = new();
}
