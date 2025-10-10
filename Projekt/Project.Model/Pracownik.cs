using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pracownik
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Id { get; set; }
        public required int Age { get; set; }
    }
}
