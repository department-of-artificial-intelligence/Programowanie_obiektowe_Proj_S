using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public interface ICinemaRepository
    {
        void Add(Cinema cinema);
        Cinema GetByID(int iD);
        IReadOnlyList<Cinema> GetAll();
        void DeleteByID(int iD);
    }
}
