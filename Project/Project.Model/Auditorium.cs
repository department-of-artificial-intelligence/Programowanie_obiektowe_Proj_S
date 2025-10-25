using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project.Entities;
using Project.Interfaces;
using Project.Utils;

namespace Project.Models
{
    public class Auditorium : BaseEntity, IRating
    {
        public string Id { get; private set; }
        public string CinemaId { get; private set; }
        public string AuditoriumName { get; private set; }
        public uint RoomNumber { get; private set; }
        public uint Rows {  get; private set; }
        public uint SeatsPerRow { get; private set; }
        public uint MaxCapacity { get; private set; }
        public List<string> Features { get; private set; } = [];
        public double Rating { get; private set; } = 0;
        public uint CustomersRated { get; private set; } = 0;

        public void UpdateRating(uint mark)
        {
            Rating = RatingHandler.CalculateRating(CustomersRated, Rating, mark);
            CustomersRated++;

            this.MarkAsUpdated();
        }

        public Auditorium()
        {
            throw new NotImplementedException("Cannot create an epty Auditorium obj");
        }

        public Auditorium
        (
            string cinemaId, 
            string auditoriumName, 
            uint roomNumber, 
            uint rows, 
            uint seatsPerRow 
        )
        {
            this.Id = IdHandler.CreateId();
            this.CinemaId = cinemaId;
            this.AuditoriumName = auditoriumName;
            this.RoomNumber = roomNumber;
            this.Rows = rows;
            this.SeatsPerRow = seatsPerRow;
            this.MaxCapacity = rows * seatsPerRow;
        }

        public Auditorium
(           string id,
            string cinemaId,
            string auditoriumName,
            uint roomNumber,
            uint rows,
            uint seatsPerRow,
            double rating,
            uint customersRated,
            List<string> features,
            DateTime updatedAt,
            DateTime createdAt
)
        {
            this.Id = id;
            this.CinemaId = cinemaId;
            this.AuditoriumName = auditoriumName;
            this.RoomNumber = roomNumber;
            this.Rows = rows;
            this.SeatsPerRow = seatsPerRow;
            this.MaxCapacity = rows * seatsPerRow;
            this.Rating = rating;
            this.CustomersRated = customersRated;
            this.Features = features;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;

        }

        public bool AddFeature(string feature)
        {
            if (!ArrayHandler.AddUniqueStringToMax5NlementsArray(this.Features, feature, 5))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public string GetAllFeatures()
        {
            return ArrayHandler.StringArrayToString(this.Features);
        }

        public bool DeleteFeature(string feature)
        {
            if (!ArrayHandler.DeleteElFromStringArray(this.Features, feature))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public override string ToString()
        {
            return $"Auditorium Id: {this.Id} \n" +
                   $"Cinema Id: {this.CinemaId} \n" +
                   $"Auditorium Name: {this.AuditoriumName} \n" +
                   $"Room number: {this.RoomNumber} \n" +
                   $"Rows: {this.Rows} \n" +
                   $"Seats per row: {this.SeatsPerRow} \n" +
                   $"Max capacity: {this.MaxCapacity} \n" +
                   $"Rating: {this.Rating} \n" +
                   $"Customers rated: {this.CustomersRated} \n" +
                   $"Features: {this.GetAllFeatures()} \n" +
                   $"updated_at: {this.UpdatedAt} \n" +
                   $"created_at: {this.CreatedAt} \n";
        }

        public void UpdateGlobalInfo
        (
            string auditoriumName,
            uint roomNumber,
            uint rows,
            uint seatsPerRow
        )
        {
            this.AuditoriumName = auditoriumName;
            this.RoomNumber = roomNumber;
            this.Rows = rows;
            this.SeatsPerRow = seatsPerRow;
            this.MaxCapacity = rows * seatsPerRow;

            this.MarkAsUpdated();
        }
    }
}
