using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Interfaces;

public interface IDishIngridientService
{
    void ChangeQuantity(DishIngridient dishIngridient, decimal quintity);
    void ChangeIngridient(DishIngridient dishIngridient, Ingridient ingridient);

}