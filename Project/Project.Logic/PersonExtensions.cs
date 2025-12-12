using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Logic
{
    public static class PersonExtensions
    {
        public static string FullName(this Person person)
            => $"{person.FirstName} {person.LastName}";
    }
}
