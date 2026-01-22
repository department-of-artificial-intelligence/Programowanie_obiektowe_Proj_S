using Project.Abstractions;

namespace Project.Model
{
    public class Order : IOrder
    {
        public int Id { get; private set; }
        public string LoadingDescription { get; private set; }
        public string LoadingAddress { get; private set; }
        public string UnloadingAddress { get; private set; }

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Driver? AssignedDriver { get; private set; }
        public Vehicle? AssignedVehicle { get; private set; }

        string IOrder.LoadDesc => LoadingDescription;
        string IOrder.UnloadingAdress => UnloadingAddress;
        IDriver? IOrder.AssignedDriver => AssignedDriver;
        IVehicle? IOrder.AssignedVehicle => AssignedVehicle;

        public Order() { }
        public Order(int id, string loadDesc, string loadingAddress, string unloadingAddress)
        {
            Id = id;
            LoadingDescription = loadDesc;
            LoadingAddress = loadingAddress;
            UnloadingAddress = unloadingAddress;
        }

        public void AssignOrder(IDriver driver)
        {
            if (driver is Driver concreteDriver)
            {
                if (Status != OrderStatus.Pending)
                {
                    Console.WriteLine($"ERROR - Order {Id} cannot be assigned. Only 'pending' orders can be assigned.");
                    return;
                }

                if (concreteDriver.AssignedVehicle == null)
                {
                    Console.WriteLine($"ERROR - Driver {concreteDriver.FirstName} cannot take order without a vehicle!");
                    return;
                }

                if (!concreteDriver.IsAvailable)
                {
                    Console.WriteLine($"ERROR - Driver {concreteDriver.FirstName} is busy or unavailable.");
                    return;
                }

                AssignedDriver = concreteDriver;
                AssignedVehicle = concreteDriver.AssignedVehicle;
                Status = OrderStatus.InProgress;

                concreteDriver.AssignOrder(this);
                Console.WriteLine($"Order {Id} has been assigned to driver {concreteDriver.FirstName} {concreteDriver.LastName} and is now in progress.");
            }
            else
            {
                throw new ArgumentException("ERROR - Provided driver is not a concrete Driver type.");
            }
        }

        public override string ToString()
        {
            return $"Order with ID: {Id} \n" +
                   $"Loading Address: {LoadingAddress}\n" +
                   $"Unloading Address: {UnloadingAddress}\n" +
                   $"Loading Description: {LoadingDescription}";
        }
    }
}