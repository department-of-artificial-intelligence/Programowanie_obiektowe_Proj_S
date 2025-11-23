using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly List<Cinema> _cinemas;
        private int _nextId = 1;

        public CinemaRepository(List <Cinema> inicijalizacja)
        {
            _cinemas = inicijalizacja;
            if (_cinemas.Any())
                _nextId = _cinemas.Max(c => c.CinemaID) + 1;
        }

        public void Add(Cinema cinema) 
        {
            cinema.CinemaID = _nextId;
            _cinemas.Add(cinema);
        }

        public Cinema GetByID(int id) 
        {
            return _cinemas.FirstOrDefault(x=> x.CinemaID == id )!;
        }

        public IReadOnlyList<Cinema> GetAll()
        {
            return _cinemas.AsReadOnly();
        }

        public void DeleteByID(int id)
        {
            var cinemaToRemove = GetByID(id);
            if (cinemaToRemove is not null )
            {
                _cinemas.Remove(cinemaToRemove);
            }
        }
    }
}
