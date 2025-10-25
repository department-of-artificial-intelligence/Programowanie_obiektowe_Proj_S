using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project.Entities;
using Project.Utils;

namespace Project.Models
{
    public class Seance : BaseEntity
    {
        public string Id {get; private set;}
        public string FilmId {get; private set;}
        public string AuditoriumId {get; private set;}
        public DateTime StartTime {get; private set;}
        public DateTime EndTime {get; private set;}
        public double Price {get; private set;}
        public List<string> OccupiedSeatsId { get; private set; } = [];

        public Seance()
        {
            throw new NotImplementedException("Cannot create an epty Seance obj");
        }

        public Seance
        (
            string filmId,
            string auditoriumId,
            DateTime startTime,
            double price,
            Film film
        )
        {
            this.Id = IdHandler.CreateId();
            this.FilmId = filmId;
            this.AuditoriumId = auditoriumId;
            this.StartTime = startTime;
            this.EndTime = startTime.AddMinutes(film.DurationInMinutes);
            this.Price = price;
        }

        public Seance
        (
            string id,
            string filmId,
            string auditoriumId,
            DateTime startTime,
            double price,
            List<string> occupiedSeatsId,
            DateTime updatedAt,
            DateTime createdAt,
            Film film
        )
        {
            this.Id = id;
            this.FilmId = filmId;
            this.AuditoriumId = auditoriumId;
            this.StartTime = startTime;
            this.EndTime = startTime.AddMinutes(film.DurationInMinutes);
            this.Price = price;
            this.OccupiedSeatsId = occupiedSeatsId;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }
        public bool AddOccupiedSeatId(string seatId, Auditorium aud)
        {
            if (!ArrayHandler.AddUniqueStringToMax5NlementsArray(this.OccupiedSeatsId, seatId, aud.MaxCapacity))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public string GetAllOccupiedSeatsId()
        {
            return ArrayHandler.StringArrayToString(this.OccupiedSeatsId);
        }

        public bool DeleteOccupiedSeatId(string seatId)
        {
            if (!ArrayHandler.DeleteElFromStringArray(this.OccupiedSeatsId, seatId))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public override string ToString()
        {
            return $"Seance id: {this.Id} \n" +
                   $"Film id: {this.FilmId} \n" +
                   $"Auditorium Id: {this.AuditoriumId} \n" +
                   $"Start/ End time: {this.StartTime} / {this.EndTime} \n" +
                   $"Price: {this.Price} \n" +
                   $"Occupied seats id: {this.GetAllOccupiedSeatsId()} \n" +
                   $"updated_at: {this.UpdatedAt} \n" +
                   $"created_at: {this.CreatedAt} \n";
        }

    }
}
