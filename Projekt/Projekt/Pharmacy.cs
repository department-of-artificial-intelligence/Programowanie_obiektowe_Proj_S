using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public interface IInventory
    {
        void AddProduct(Product product); 
        void DecreaseQuantity(int productId, int quantitySold); 
        List<Product> GetAllProducts(); 
    }


    public class Pharmacy
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Product> Products { get; set; } 
        public List<Employee> Employees { get; set; } 
        public List<Sale> Sales { get; set; } 

        public Pharmacy(string name, string address)
        {
            Name = name;
            Address = address;
            Products = new List<Product>();
            Employees = new List<Employee>();
            Sales = new List<Sale>();
        }

       
        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

      
        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

       
        public void RecordSale(Sale sale)
        {
            Sales.Add(sale);
        }


        public string ToStringProducts()
        {
            string str = "";
            foreach (Product product in Products)
            {
                str += product.ToString() + "\n";
            }
            return str; 
        }

        public string ToStringEmployees()
        {
            string str = "";
            foreach (Employee Employee in Employees)
            {
                str += Employee.ToString() + "\n";
            }
            return str;
        }

        public string ToStringSales()
        {
            string str = "";
            foreach (Sale Sale in Sales)
            {
                str += Sale.ToString() + "\n";
            }
            return str;
        }



    }
}
