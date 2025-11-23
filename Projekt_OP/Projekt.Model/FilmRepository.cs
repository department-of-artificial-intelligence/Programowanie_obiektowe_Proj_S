using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class FilmRepository : IFilmRepository
    {
        private List<Film> _films;
        private int _nextId = 1;

        public FilmRepository(List<Film> films) 
        {
            _films = films;
            if (_films.Any())
            {
                _nextId = _films.Max(x => x.ID) + 1;
            }
        }

        public void Add(Film film)
        {
            film.ID = ++_nextId;
            _films.Add(film);

        }

        public Film GetByID(int id) 
        {
            return _films.FirstOrDefault(x => x.ID == id);
        }

        public Film GetByName(string title) 
        {
            return _films.FirstOrDefault(x => x.Title == title);
        }

        public IReadOnlyList<Film> GetAll()
        {
            return _films.AsReadOnly();
        }

    }
}
