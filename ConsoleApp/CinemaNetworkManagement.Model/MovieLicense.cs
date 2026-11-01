using System;

namespace CinemaNetworkManagement.Model
{
    public class MovieLicense
    {
        public int Id { get; }
        public Movie Movie { get; }
        public decimal Cost { get; }
        public DateTime ValidFrom { get; }
        public DateTime ValidTo { get; }

        public MovieLicense(int id, Movie movie, decimal cost, DateTime validFrom, DateTime validTo)
        {
            Id = id;
            Movie = movie;
            Cost = cost;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

    }
}
