using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Model
{
    public class Office
    {
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public ICollection<Trip> Trips { get; set; }

        public Trip Trip
        {
            get => default; set { }
        }
    }
}
