using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsRental.Model;

namespace CarsRental.Logic
{
    public interface IReservationService
    {
        bool CreateReservation(Reservation reservation);
        Reservation? GetReservation(int reservationId);
        void RemoveReservation(int reservationId);
        List<Reservation> GetAllReservations();
        double CalculateCost(int reservationId);
    }
}
