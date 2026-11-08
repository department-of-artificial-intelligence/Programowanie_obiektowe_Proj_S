using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public class OrderItem
{
    public Dish Dish {  get; set; }
    public int Quantity { get; set; }


    public OrderItem(Dish dish, int quantity)
    {
        Dish = dish;
        Quantity = quantity;
    }

    public decimal GetLineTotal()
    {
        return Quantity * Dish.Price;
    }
}

