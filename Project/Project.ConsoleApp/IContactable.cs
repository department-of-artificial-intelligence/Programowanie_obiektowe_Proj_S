using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ConsoleApp
{
    public interface IContactable
    {
        void UpdateContactInfo(string phone, string email);
    }
}
