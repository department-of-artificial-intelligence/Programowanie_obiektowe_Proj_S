using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public Reservation? Reservation { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public Payment(int id, Reservation reservation, decimal amount)
        {
            Id = id;
            Reservation = reservation;
            Amount = amount;
            PaymentDate = DateTime.Now;
            Status = PaymentStatuses.Unpaid;
        }
        public Payment()
        {
            Id = 0;
            Reservation = null;
            Amount = 0m;
            PaymentDate = DateTime.MinValue;
            Status = PaymentStatuses.Unpaid;
        }
        public bool PostPayment()
        {
            if (Status == PaymentStatuses.Unpaid)
            {
                Status = PaymentStatuses.Paid;
                return true;
            }
            return false;
        }
        public override string ToString()
            => $"[Płatność ID {Id}]: Kwota: {Amount:C}, Data: {PaymentDate:dd-MM-yyyy HH:mm}, Status: {Status}";
    }
}
