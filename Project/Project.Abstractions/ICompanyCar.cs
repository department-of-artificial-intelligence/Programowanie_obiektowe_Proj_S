namespace Project.Abstractions
{
    public interface ICompanyCar : IVehicle
    {
        bool IsExecutive { get; }
    }
}