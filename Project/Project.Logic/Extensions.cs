using Project.Model;
namespace Project.Logic;

public static class Extensions
{
    public static bool IsSoldOut(this Concert concert)
    {
        if (concert.Venue.Capacity > concert.TicketsSold) return false;
        return true;
    }
}