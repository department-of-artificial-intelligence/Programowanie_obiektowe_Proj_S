using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.DataModels
{
    public class DishIngridient
    {
        public Ingridient Ingridient { get; set; }
        public decimal Quantity { get; set; }

        public DishIngridient(Ingridient ingridient, decimal quantity)
        {
            Ingridient = ingridient;
            Quantity = quantity;
        }

        public void ChangeQuantity(decimal quintity)
        {
            if (quintity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quintity), "must be > 0");
            Quantity = quintity;
        }

        public void ChangeIngridient(Ingridient ingridient)
        {
            ArgumentNullException.ThrowIfNull(ingridient);
            Ingridient = ingridient;
        }
    }
}
