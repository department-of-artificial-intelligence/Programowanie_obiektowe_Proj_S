namespace Project.Interfaces
{
    public interface IRating
    {
        double Rating { get; }
        uint CustomersRated { get; }

        void UpdateRating(uint mark);
    }
}
