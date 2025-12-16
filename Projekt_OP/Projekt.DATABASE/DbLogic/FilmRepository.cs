using Microsoft.EntityFrameworkCore;
using Projekt.DATABASE;
using Projekt.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model.Logic
{
    public class FilmRepository : IFilmRepository
    {
        private readonly List<Film>? _films;
        private readonly ApplicationDbContext? _context;

        public FilmRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public void Add(Film film)
        {
            _context.Films.Add(film);
            _context.SaveChanges();
        }
        public void Remove(Film film) 
        {
            _context.Films.Remove(film);
            _context.SaveChanges(); 
        }

        public Film GetByID(int id) 
        {
            var film = _context.Films
                .Include(f => f.Hall)
                .Include(f => f.Hall.Cinema)
                .FirstOrDefault(f => f.ID == id);
            if (film == null)
            {
                throw new ArgumentException($"Film with ID {id} not found.F3");
            }
            return film;

        }

        public Film GetByName(string title) 
        {
            var film = _context.Films
                .Include(f => f.Hall)
                .Include(f => f.Hall.Cinema)
                .FirstOrDefault(f => f.Title == title);
            if (film == null)
            {
                throw new ArgumentException($"Film with Title: {title} not found.F4");
            }
            return film;
        }

        public IReadOnlyList<Film> GetAll()
        {
            var films = _context.Films
                .Include(f => f.Hall)
                .Include(f => f.Hall.Cinema)
                .ToList()
                .AsReadOnly();
            return films;
        }

    }
}
