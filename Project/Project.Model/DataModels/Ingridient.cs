using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.DataModels
{
    public class Ingridient
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Unit {  get; set; }
        public decimal UnitPrice { get; set; }
    }
}
