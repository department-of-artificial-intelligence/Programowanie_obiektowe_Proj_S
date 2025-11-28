using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Interfaces;

namespace Pharmacy.Models
{
    public class Worker : Person
    {
        public string Position { get; set; }


        public Worker(int id, string firstName, string lastName , string position) :base( id,  firstName = "Nieznany",  lastName = "Nieznany")
        {
            Position = position;
        }
    }
}
