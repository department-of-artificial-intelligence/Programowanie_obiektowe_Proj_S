using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Interfaces
{
    public interface IRentable
    {
        bool IsAvailable {  get; }
        void Rent();
        void Return();
    }
}
