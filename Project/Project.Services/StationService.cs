using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using Project.DAL;
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class StationService : IStationService
    {
        private readonly ApplicationDbContext _context;

        public StationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddStation(string name, string city, string address, int capacity)
        {
            
            var station = new Station(name, city, address, capacity);
            _context.Stations.Add(station);
            _context.SaveChanges();
            Console.WriteLine($"Station '{name}' (Limit: {capacity}) created!");
        }

        public List<Station> GetAllStations()
        {
            return _context.Stations.Include(s => s.Bicycles).ToList();
        }

        public bool ParkBicycle(int stationId, int bicycleId)
        {
            
            var station = _context.Stations.Include(s => s.Bicycles).FirstOrDefault(s => s.Id == stationId);
            var bike = _context.Bicycles.FirstOrDefault(b => b.Id == bicycleId);

            if (station == null || bike == null) return false;

            
            if (station.Bicycles.Count >= station.Capacity)
            {
                Console.WriteLine("Station is full!");
                return false;
            }

            
            try
            {
                bike.Return(station);

                _context.SaveChanges(); 
                Console.WriteLine($"Bike parked at {station.Name}.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}