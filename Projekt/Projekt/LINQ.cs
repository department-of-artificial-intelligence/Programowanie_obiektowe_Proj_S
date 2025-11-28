using Projekt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Projekt
{
        public class LINQ{

        public List<Product> Products { get; set; }

        public List<Product> Search(List<Product> products, string search)
        {
            Products = products;
            var results=Products.Where(u=>u.Name.Equals(search)).ToList();
            return results;
        }
    
    }


}