

using Project.Model.DataModels;

namespace Project.Services.Services
{
    public class DishIngridientService
    {
        public void ChangeQuantity(DishIngridient dishIngridient, decimal quintity)
        {
            if (quintity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quintity), "must be > 0");
            dishIngridient.Quantity = quintity;
        }

        public void ChangeIngridient(DishIngridient dishIngridient, Ingridient ingridient)
        {
            ArgumentNullException.ThrowIfNull(ingridient);
            dishIngridient.Ingridient = ingridient;
        }

    }
}
