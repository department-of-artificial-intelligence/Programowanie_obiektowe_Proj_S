using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public interface IDriver
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool IsAvailable { get; set; }

        Vehicle? AssignedVehicle { get; }

        void AssignVehicle(Vehicle vehicle);
        void CompleteOrder();

    }
}
