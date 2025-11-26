using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Abstractions
{
    public interface IOrder
    {
        int Id { get;}
        string LoadDesc { get; }
        string LoadingAddress { get; }
        string UnloadingAdress { get; }
        OrderStatus Status { get; }
        Driver? AssignedDriver { get; }
        Vehicle? AssignedVehicle { get; }

        void AssignOrder(IDriver driver);
    }
}
