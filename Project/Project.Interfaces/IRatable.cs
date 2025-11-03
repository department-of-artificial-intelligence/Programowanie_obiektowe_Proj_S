namespace Project.Interfaces
{
    public interface IRatable
    {
        double Rating { get; }
        uint TotalRatings { get; }
        
        void AddRating(uint rating);
    }
}
