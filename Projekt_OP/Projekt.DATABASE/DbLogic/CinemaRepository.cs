using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Model.Interfaces;
using Projekt.Model;
using Microsoft.EntityFrameworkCore;

namespace Projekt.DATABASE.Logic
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly ApplicationDbContext? _context;

        public CinemaRepository(ApplicationDbContext context)
        { 
            _context = context;
        }

        public void Add(Cinema cinema) 
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:C1");
            }
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

        }

        public void Remove(Cinema cinema) 
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:C2");
            }
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();

        }  

        public Cinema GetByID(int id) 
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:C3");
            }

            var cinema = _context.Cinemas
                .Include(c => c.Address)
                .Include(c => c.Hall)
                .Include(c => c.Employees)
                .FirstOrDefault(c => c.CinemaID == id);
            if (cinema is null)
            {
                throw new Exception($"Cinema with this id: {id} not found.ERROR:C3");
            }

            return cinema;
        }

        public IReadOnlyList<Cinema> GetAll()
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:C4");
            }

            return _context.Cinemas
                .Include(c => c.Address)
                .Include(c => c.Hall)
                .Include(c => c.Employees)
                .ToList()
                .AsReadOnly();
        }

        public void DeleteByID(int id)
        {
            var cinemaToRemove = GetByID(id);
            if (cinemaToRemove is not null )
            {
                if (_context is null)
                {
                    throw new Exception("Database context is not initialized.ERROR:C5");
                }
                _context.Cinemas.Remove(cinemaToRemove);
            }
        }
    }
}
