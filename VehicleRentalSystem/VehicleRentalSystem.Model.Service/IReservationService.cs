using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Service
{
    public interface IReservationService
    {
        Task<Reservation?> CreateReservation(int customerId, int vehicleId, DateTime startDate, DateTime endDate);
        Task<Reservation?> GetReservation(int reservationId);
        Task<bool> CompleteReservation(int reservationId);
        Task<List<Reservation>> GetAllReservations();
        Task<bool> CancelReservation(int reservationId);
        Task<double> CalculateCost(int reservationId);
        Task<int> GetReservationCount();
        Task<int> GetActiveReservationCount();
        Task<int> GetCompletedReservationCount();
        Task<int> GetCanceledReservationCount();
    }
}

