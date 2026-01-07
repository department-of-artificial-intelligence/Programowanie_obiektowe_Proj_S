using System;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public class CourierDelivery : IDeliveryMethod
    {
        public string TrackingId { get; set; }
        public required string ShippingAddress { get; set; }

        public string Name => "Kurier DPD/DHL";
        public decimal Cost => 19.99m;
        public int EstimatedDays => 1;

        public CourierDelivery()
        {
            TrackingId = "DPD-" + new Random().Next(100000, 999999);
        }

        [SetsRequiredMembers]
        public CourierDelivery(string address)
        {
            ShippingAddress = address;
            TrackingId = "DPD-" + new Random().Next(100000, 999999);
        }

        public string GetDeliveryDetails()
        {
            return $"[{Name}] Adres: {ShippingAddress} | Koszt: {Cost:C} | Czas: {EstimatedDays} dni | Tracking: {TrackingId}";
        }
    }
}