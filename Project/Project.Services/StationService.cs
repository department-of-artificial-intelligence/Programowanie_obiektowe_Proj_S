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

        public string ParkBicycle(int stationId, int bicycleId)
        {
            
            var station = _context.Stations.Include(s => s.Bicycles).FirstOrDefault(s => s.Id == stationId);
            var bike = _context.Bicycles.Find(bicycleId);

            if (station == null) return "Error: Station not found.";
            if (bike == null) return "Error: Bike not found.";
            if (station.Bicycles.Count >= station.Capacity) return "Error: Station is full.";

            
            if (bike.Status == BicycleStatus.Rented)
            {
                return "Error: Bike is currently rented! Customer must return it first.";
            }

            try
            {
                
                bike.Park(stationId);

                _context.SaveChanges();
                return $"Success: Bike '{bike.Model}' parked at '{station.Name}'.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}