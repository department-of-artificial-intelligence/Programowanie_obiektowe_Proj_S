using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class RentalService : IRentalService
    {
        private readonly ApplicationDbContext _context;

        public RentalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string RentBicycle(int bicycleId, int customerId)
        {
            var bike = _context.Bicycles.Find(bicycleId);
            var customer = _context.Customers.Find(customerId);

            if (bike == null) return "Bicycle not found.";
            if (customer == null) return "Customer not found.";

            
            if (bike.Status != BicycleStatus.Available) return "Bike is already rented.";

            try
            {
                
                bike.Rent();

               
                bike.CustomerId = customerId;

               
                var record = new RentalRecord
                {
                    BicycleId = bicycleId,
                    CustomerId = customerId,
                    RentDate = DateTime.Now,
                    ReturnDate = null,
                    TotalCost = null
                };

                _context.Rentals.Add(record);
                _context.SaveChanges();
                return "Bike rented successfully!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string ReturnBicycle(int bicycleId, int stationId)
        {
            
            var record = _context.Rentals
                .FirstOrDefault(r => r.BicycleId == bicycleId && r.ReturnDate == null);

            
            var bike = _context.Bicycles.Find(bicycleId);
            var station = _context.Stations
                .Include(s => s.Bicycles)
                .FirstOrDefault(s => s.Id == stationId);

           
            if (bike == null) return "Bike not found.";
            if (station == null) return "Station not found.";
            if (record == null) return "No active rental for this bike.";
            if (station.Bicycles.Count >= station.Capacity) return "Station is full.";

            try
            {
                
                record.ReturnDate = DateTime.Now;

                
                TimeSpan duration = record.ReturnDate.Value - record.RentDate;

                
                decimal hoursRented = (decimal)duration.TotalHours;

                

                record.TotalCost = hoursRented * bike.Price;

                
                bike.Return(station);
                bike.CustomerId = null; 

                
                _context.SaveChanges();

              
                string timeSpent = duration.ToString(@"hh\:mm\:ss");

                return $"SUCCESS!\n" +
                       $"Bike: {bike.Model}\n" +
                       $"Time: {timeSpent} ({hoursRented:F2} hours)\n" +
                       $"Rate: {bike.Price} PLN/h\n" +
                       $"TOTAL TO PAY: {record.TotalCost:F2} PLN";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public List<RentalRecord> GetHistory()
        {
            return _context.Rentals
                .Include(r => r.Bicycle)
                .Include(r => r.Customer)
                .OrderByDescending(r => r.RentDate)
                .ToList();
        }
    }
}