using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Interfaces;

public interface IIngridientService
{
    public void UpdatePrice(Ingridient ingridient, decimal newPrice);
}