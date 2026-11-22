
using Project.Model.DataModels;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class IngridientService : IIngridientService
    {
        public void UpdatePrice(Ingridient ingridient, decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(newPrice), "must be > 0");
            ingridient.UnitPrice = newPrice;
        }
    }
}
