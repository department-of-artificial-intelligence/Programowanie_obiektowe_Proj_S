using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class Festival
    {
        public int Id{  get; set; }
        public Venue Venue { get; set; }
        public Artist[] Artists {  get; set; }
    }
}
