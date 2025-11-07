using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model
{
    public class Management
    {
        public List<Biuro> Agencies { get; set; }
        public List<Excursion> Excursions { get; set; }

        public Management()
        { 
            Agencies = new List<Biuro>();
            Excursions = new List<Excursion>();
        }


    }
}
