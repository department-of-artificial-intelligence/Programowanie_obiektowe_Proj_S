public interface IDriver
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    bool IsAvailable { get; }
    object? AssignedVehicle { get; }

    void AssignVehicle(object vehicle);
    void CompleteOrder();
    void Print();
}
