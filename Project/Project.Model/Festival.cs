using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Festival
    {
        public int Id{  get; set; }
        public string Name{ get; set; }
        public Venue Venue { get; set; }
        public Artist[] Artists {  get; set; }

        public override string ToString()
        {
            return $"{Name}: {string.Join("/n ",Artists.Select(a => a.ToString()))}, {Venue}";
        }
    }
}
