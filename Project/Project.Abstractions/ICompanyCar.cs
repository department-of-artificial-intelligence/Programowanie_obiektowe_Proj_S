namespace Project.Abstractions
{
    public interface ICompanyCar : IVehicle
    {
        public int NumberOfSeats { get; init; }
    }
}