using System.Collections.Generic;
using System.Linq;
using Project.Model;

namespace Project.DAL
{
    public class CatalogService : ICatalogService
    {
        private readonly ApplicationDbContext _context;
        public CatalogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Subject AddSubject(string name, string description) {
            var newSubject = new Subject(name, description);
            _context.Subjects.Add(newSubject);
            _context.SaveChanges();
            return newSubject;
        }

        public List<Subject> GetSubjects() => _context.Subjects.ToList();
    }
}

