using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public class Cinema
    {
        private readonly List<ScreeningRoom> _rooms;
        private readonly List<Customer> _customers;

        public int Id { get; }
        public string Name { get; set; }
        public Address Address { get; }
        public bool IsActive { get; private set; }

        public List<ScreeningRoom> Rooms
        {
            get { return new List<ScreeningRoom>(_rooms); }
        }

        public List<Customer> Customers
        {
            get { return new List<Customer>(_customers); }
        }

        public Cinema(int id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
            IsActive = true;
            _rooms = new List<ScreeningRoom>();
            _customers = new List<Customer>();
        }

    
    }
}
