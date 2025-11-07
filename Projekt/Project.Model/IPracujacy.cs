using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public interface Ipracujacy
    {
        public string StanowiskoPracy { get; set; }
        List<Projekt> ListaProjektow {  get; set; }

        void PassedProjects();

        void NotPassedProjects();
    }
}
