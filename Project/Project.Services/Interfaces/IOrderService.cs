using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Interfaces;

public interface IOrderService
{
    decimal ProcessOrder(Order order);

    void AddItem(Order order, Dish dish, int quantity);

    void RemoveItem(Order order, int dishId);

    decimal CalculateTotal(Order order);

    void ChangeStatus(Order order, OrderStatus status);

    void Cancel(Order order);
}