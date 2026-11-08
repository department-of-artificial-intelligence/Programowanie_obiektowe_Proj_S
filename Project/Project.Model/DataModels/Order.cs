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

    public void AddItem(Dish dish, int quantity)
    {
        ArgumentNullException.ThrowIfNull(dish);
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException("quantity can`t be < 1");
        Items.Add(new OrderItem(dish, quantity));

    }

    public void RemoveItem(int dishId)
    {
        OrderItem? founded = Items.Find(i => i.Dish.Id == dishId);
        if (founded == null)
            throw new ArgumentException("Order item doesn`t found");
        Items.Remove(founded);
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(i => i.GetLineTotal());
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
    }

    public void Cancel()
    {
        Status = OrderStatus.Canceled;
    }
}

