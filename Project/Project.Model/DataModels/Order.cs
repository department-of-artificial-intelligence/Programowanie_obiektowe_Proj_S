using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;


public enum OrderStatus
{
    Done,
    InProgress,
    Canceled
}

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> Items { get; set; }
    public Customer Customer { get; set; }
    public Restaurant Place { get; set; }
    public decimal Price { get; set; }


    public Order(int id, DateTime createdAt, OrderStatus status, List<OrderItem> items, Customer customer, Restaurant place, decimal price)
    {
        Id = id;
        CreatedAt = createdAt;
        Status = status;
        Items = items;
        Customer = customer;
        Place = place;
        Price = price;


    }
}

