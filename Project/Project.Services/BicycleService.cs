using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class BicycleService : IBicycleService
    {
        private readonly ApplicationDbContext _context;

        public BicycleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBicycle(string model, BicycleType type, decimal price, int? batteryLevel = null, int? rangeKm = null)
        {
            var bike = new Bicycle { 
                Model = model, 
                Type = type, 
                Price = price, 
                BatteryLevel = batteryLevel,
                RangeKm = rangeKm
            };
            _context.Bicycles.Add(bike);
            _context.SaveChanges();
            Console.WriteLine($"{type} bike '{model}' saved!");
        }

        
        public void ShowCheapBikes()
        {
            var bikes = GetCheapBicycles(); 
            foreach (var b in bikes) Console.WriteLine(b);
        }

        
        public List<Bicycle> GetCheapBicycles()
        {
            return _context.Bicycles
                .Where(b => b.Price < 20)
                .OrderBy(b => b.Price)
                .ToList();
        }

        public List<Bicycle> GetAllBicycles()
        {
        return _context.Bicycles
            .Include(b => b.CurrentStation) 
            .ToList();
        }

        
        public void ShowStatistics() { }
    }
}