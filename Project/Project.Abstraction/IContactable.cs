using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstraction
{
    public interface IContactable
    {
        string? Email { get; }
        string? Phone { get; }
    }
}
