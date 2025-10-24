using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaSamochodow.Model
{
    interface ICustomer
    {
        void ShowCustomers();
        void AddCustomer(Customer customer);
    }

    public class Customer
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int LicenseNumber {  get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Prawo jazdy: {LicenseNumber}";
        }
    }
}
