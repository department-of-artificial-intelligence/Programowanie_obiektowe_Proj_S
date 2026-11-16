using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Model
{
    public class Trip
    {
        public ICollection<Attraction> Attractions { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public string Name { get; set; }
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        public TourGuide TourGuide { get; set; }
        public int TourGuideId { get; set; }
        public int TripId { get; set; }

        public TourGuide TourGuide1
        {
            get => default; set { }
        }

        public Customer Customer
        {
            get => default; set { }
        }

        public Attraction Attraction
        {
            get => default; set {}
        }
    }
}
