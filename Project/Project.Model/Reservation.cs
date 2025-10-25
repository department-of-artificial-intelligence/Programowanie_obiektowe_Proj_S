using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project.Entities;
using Project.Utils;

namespace Project.Models
{
    public class Reservation : BaseEntity
    {
        public string Id { get; private set; }
        public string SeanceId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerSurname { get; private set; }
        public string CustomerEmail { get; private set; }
        public string CustomerPhone { get; private set; }
        public string PaymentMethod { get; private set; }

        public Reservation()
        {
            throw new NotImplementedException("Cannot create an epty Reservation obj");
        }

        public Reservation
        (
            string seanceId,
            string customerName,
            string customerSurname,
            string customerEmail,
            string customerPhone,
            string paymentMethod    
        )
        {
            this.Id = IdHandler.CreateId();
            this.SeanceId = seanceId;
            this.CustomerName = customerName;
            this.CustomerSurname = customerSurname;
            this.CustomerEmail = customerEmail;
            this.CustomerPhone = customerPhone;
            this.PaymentMethod = paymentMethod;
        }

        public Reservation
        (
            string id,
            string seanceId,
            string customerName,
            string customerSurname,
            string customerEmail,
            string customerPhone,
            string paymentMethod,
            DateTime updatedAt,
            DateTime createdAt
        )
        {
            this.Id = id;
            this.SeanceId = seanceId;
            this.CustomerName = customerName;
            this.CustomerSurname = customerSurname;
            this.CustomerEmail = customerEmail;
            this.CustomerPhone = customerPhone;
            this.PaymentMethod = paymentMethod;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"Reservation Id: {this.Id} \n" +
                   $"Seance Id: {this.SeanceId} \n" +
                   $"Customer Name / Surname: {this.CustomerName} / {this.CustomerSurname} \n" +
                   $"Customer email: {this.CustomerEmail} \n" +
                   $"Customer phone: {this.CustomerPhone} \n" +
                   $"Payment methot: {this.PaymentMethod} \n" +
                   $"updated_at: {this.UpdatedAt} \n" +
                   $"created_at: {this.CreatedAt} \n";
        }
    }
}
