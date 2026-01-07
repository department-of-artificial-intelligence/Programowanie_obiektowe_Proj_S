using System;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public class ParcelLockerDelivery : IDeliveryMethod
    {
        public string TrackingId { get; set; }
        public required string LockerCode { get; set; }

        public string Name => $"Paczkomat InPost ({LockerCode})";
        public decimal Cost => 12.99m;
        public int EstimatedDays => 2;

        public ParcelLockerDelivery()
        {
            TrackingId = "PACK-" + new Random().Next(100000, 999999);
        }

        [SetsRequiredMembers]
        public ParcelLockerDelivery(string lockerCode)
        {
            LockerCode = lockerCode;
            TrackingId = "PACK-" + new Random().Next(100000, 999999);
        }

        public string GetDeliveryDetails()
        {
            return $"[{Name}] Odbiór w punkcie: {LockerCode} | Koszt: {Cost:C} | Tracking: {TrackingId}";
        }
    }
}