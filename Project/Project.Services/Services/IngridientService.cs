
using Project.Model.DataModels;

namespace Project.Services.Services
{
    public class IngridientService
    {
        public void UpdatePrice(Ingridient ingridient, decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(newPrice), "must be > 0");
            ingridient.UnitPrice = newPrice;
        }
    }
}
