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
    public class Cinema : BaseEntity, IRating
    {
        public string Id { get; private set; }
        public string CinemaName { get; private set; }
        public string Adress { get; private set; }
        public string ContactNumber { get; private set; }
        public string ContactEmail { get; private set; }
        public string ManagerName { get; private set; }

        public List<string> AvalibleFilms { get; private set; } = [];

        public double Rating { get; private set; } = 0;
        public uint CustomersRated { get; private set; } = 0;

        public void UpdateRating(uint mark)
        {
            Rating = RatingHandler.CalculateRating(CustomersRated, Rating, mark);
            CustomersRated++;

            this.MarkAsUpdated();
        }

        public Cinema()
        {
            throw new NotImplementedException("Cannot create an epty Cinema obj");
        }

        public Cinema
        (
            string cinemaName,
            string adress,
            string contactNumber,
            string contactEmail,
            string managerName
        )
        {
            this.Id = IdHandler.CreateId();
            this.CinemaName = cinemaName;
            this.Adress = adress;
            this.ContactNumber = contactNumber;
            this.ContactEmail = contactEmail;
            this.ManagerName = managerName;
        }

        public Cinema
        (
            string id,
            string cinemaName,
            string adress,
            string contactNumber,
            string contactEmail,
            string managerName,
            List<string> avalibleFilms,
            double rating,
            uint customersRated,
            DateTime updatedAt,
            DateTime createdAt
        )
        {
            this.Id = id;
            this.CinemaName = cinemaName;
            this.Adress = adress;
            this.ContactNumber = contactNumber;
            this.ContactEmail = contactEmail;
            this.ManagerName = managerName;
            this.AvalibleFilms = avalibleFilms;
            this.Rating = rating;
            this.CustomersRated = customersRated;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }

        public bool AddAvalibleFilmId(string filmId)
        {
            if (!ArrayHandler.AddUniqueStringToMax5NlementsArray(this.AvalibleFilms, filmId, 5))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public string GetAllAvalibleFilmId()
        {
            return ArrayHandler.StringArrayToString(this.AvalibleFilms);
        }

        public bool DeleteAvalibleFilmId(string filmId)
        {
            if (!ArrayHandler.DeleteElFromStringArray(this.AvalibleFilms, filmId))
            {
                return false;
            }

            this.MarkAsUpdated();
            return true;
        }

        public override string ToString()
        {
            return $"Cinema Id: {this.Id} \n" +
                   $"Cinema name: {this.CinemaName} \n" +
                   $"Cinema adress: {this.Adress} \n" +
                   $"Cinema contact number: {this.ContactNumber} \n" +
                   $"Cinema contact email: {this.ContactEmail} \n" +
                   $"Cinemaa manager name: {this.ManagerName} \n" +
                   $"Cinema avalible films: {this.GetAllAvalibleFilmId()} \n" +
                   $"Cinema rating: {this.Rating} \n" +
                   $"Cinema customers rated: {this.CustomersRated} \n" +
                   $"updated_at: {this.UpdatedAt} \n" +
                   $"created_at: {this.CreatedAt} \n";
        }

        public void UpdateGlobalInfo
        (
            string cinemaName,
            string adress,
            string contactNumber,
            string contactEmail,
            string managerName
        )
        {
            this.CinemaName = cinemaName;
            this.Adress = adress;
            this.ContactNumber = contactNumber;
            this.ContactEmail = contactEmail;
            this.ManagerName = managerName;

            this.MarkAsUpdated();
        }

    }
}
