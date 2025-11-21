using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class Customer : Person
    {
        private List<Bicycle> _bicycles;

   

        public string Address { get; set; }

        public int BicycleCount { get { return _bicycles.Count; } }

        public static int MaxBicycleCount { get; private set; } = 3;

        public Customer() : this(string.Empty, string.Empty, string.Empty) { }

        public Customer(string firstName, string lastName, string address)
            : base(firstName, lastName)
        {

            Address = address;
            _bicycles = new List<Bicycle>();
        }

      
        public Customer(string firstName, string lastName, string address, List<Bicycle> bicycles)
            : base(firstName, lastName)
        {
            
            Address = address;

            if (bicycles != null)
            {
                
                _bicycles = new List<Bicycle>(bicycles.Take(MaxBicycleCount));
            }
            else
            {
                _bicycles = new List<Bicycle>();
            }
        }


        public bool AddBicycle(Bicycle bicycle)
        {
            if (bicycle == null || _bicycles.Count >= MaxBicycleCount)
            {
                return false;
            }

            foreach (Bicycle b in _bicycles)
            {
                if (b.Id == bicycle.Id)
                {
                    return false;
                }
            }

            _bicycles.Add(bicycle);
            return true;
        }

        public bool RemoveBicycle(Bicycle bicycle)
        {
            return _bicycles.Remove(bicycle);
        }

        public Bicycle? RemoveBicycle(int bicycleId)
        {
            Bicycle? bikeToRemove = null;

            foreach (Bicycle b in _bicycles)
            {
                if (b.Id == bicycleId)
                {
                    bikeToRemove = b;
                    break;
                }
            }

            if (bikeToRemove == null) return null;

            _bicycles.Remove(bikeToRemove);
            return bikeToRemove;
        }

        public override string ToString()
        {
            
            string s = string.Format("Customer: {0} {1}, Address: {2} (Bicycles rented: {3}/{4}):",
                FirstName, LastName, Address, _bicycles.Count, MaxBicycleCount);

            foreach (Bicycle b in _bicycles)
            {
                s += "\n-" + b;
            }
            return s;
        }
    }
}
