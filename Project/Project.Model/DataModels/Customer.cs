using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class Customer
{

    public int Id { get; set; }
    public string FullName { get; set; }
    public string? Phone { get; set; }
    public int loyaltyPoints { get; set; }


    public Customer(int id, string fullName, string? phone, int loyaltyPoints)
    {
        Id = id;
        FullName = fullName;
        Phone = phone;
        this.loyaltyPoints = loyaltyPoints;
    }


    //TODO!!!!!
    //public List<Order> GetOrderHistory()
    //{

    //}

}

