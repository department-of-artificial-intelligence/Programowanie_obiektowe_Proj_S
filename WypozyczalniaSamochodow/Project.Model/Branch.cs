using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaSamochodow.Model
{
    internal class Branch
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string City { get; set; }
    }
}
