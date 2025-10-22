using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Project.Model
{
    public class Order{

        public required string Number { get; set; }

        public required int NumberOfProducts { get; set;}


        public required DateTime DateOrderPlacement {  get; set; }

        public Order(string _number, int _numberOfProducts, DateTime _dateOrderPlacement)
        { 
            Number = _number;
            NumberOfProducts = _numberOfProducts;
            DateOrderPlacement = _dateOrderPlacement;

        
        }

    }
}
