namespace Project.Abstractions
    {
    public interface IDriver
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool IsAvailable { get; }
        Vehicle? AssignedVehicle { get; }

        void AssignVehicle(Vehicle vehicle);
        void CompleteOrder();
        void Print();
    }
}
