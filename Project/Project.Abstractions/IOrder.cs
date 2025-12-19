namespace Project.Abstractions
{
   
    public interface IOrder
    {
        int Id { get;}
        string LoadDesc { get; }
        string LoadingAddress { get; }
        string UnloadingAdress { get; }
        OrderStatus Status { get; }
        IDriver? AssignedDriver { get; }
        IVehicle? AssignedVehicle { get; }

        void AssignOrder(IDriver driver);

        public enum OrderStatus
        {
            Pending,
            InProgress,
            Completed,
            Cancelled
        }
    }
}