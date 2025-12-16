using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model.Interfaces
{
    public interface IFilmRepository
    {
        void Add(Film film);
        void Remove(Film film);
        Film GetByID(int id);
        Film GetByName(string name);
        IReadOnlyList<Film> GetAll();
    }
}
