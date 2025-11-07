using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Festival : Event
    {
        public string Name { get; set; }
        public Artist[] Artists {  get; set; }

        public override string ToString()
        {
            return $"{Name} {Date}: {string.Join("/n ",Artists.Select(a => a.ToString()))}, {Venue}";
        }
    }
}
