using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model.Interfaces
{
    public interface ICinemaRepository
    {
        void Add(Cinema cinema);
        void Remove(Cinema cinema);
        Cinema GetByID(int iD);
        IReadOnlyList<Cinema> GetAll();
        void DeleteByID(int iD);
    }
}
