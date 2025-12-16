namespace Project.Model;

public class Ticket
{
    // Właściwości
    public int TicketId { get; private set; } // PK
    public decimal Price { get; private set; }
    public Performance Performance { get; private set; } = default!; 
    public Seat Seat { get; private set; } = default!; 
    public TicketStatus Status { get; set; }
    public Customer? Customer { get; set; }

    // Konstruktory
    private Ticket() { }

    internal Ticket(decimal price, Performance performance, Seat seat)
    {
        if (performance is null) throw new ArgumentNullException(nameof(performance), "Przedstawienie nie może być null");
        if (seat is null) throw new ArgumentNullException(nameof(seat), "Siedzenie nie może być null");
        if (price < 0) throw new ArgumentException("Cena biletu nie może być ujemna", nameof(price));
        Price = price;
        Performance = performance;
        Seat = seat;
        Status = TicketStatus.Available;
        Customer = null;
    }

    // Metody sprawdzające sprawdzający możliwości zarządzania dla klienta
    public bool CanBeReserved(Customer? customer)
    {
        if (customer is null) return false;
        return Status == TicketStatus.Available;
    }
    public bool CanBeCanceled(Customer? customer)
    {
        if (customer is null) return false;
        return Status == TicketStatus.Reserved && Customer == customer;
    }
    public bool CanBeBought(Customer? customer)
    {
        if (customer is null) return false;
        return Status == TicketStatus.Available || (Status == TicketStatus.Reserved && Customer == customer);
    }
    public bool CanBeRefunded(Customer? customer)
    {
        if (customer is null) return false;
        return Status == TicketStatus.Sold && Customer == customer && ( Performance.Status == PerformanceStatus.Scheduled || Performance.Status == PerformanceStatus.Canceled );
    }

    // Metody string
    public override string ToString()
    {
        string customer = Customer is not null ? (Customer.FirstName + " " + Customer.LastName) : "nieznany";
        return $"{TicketId}/Sztuka:{Performance.Play.Title}/{Price}PLN/{Status}/Sala:{Performance.Hall?.HallId}/Siedzenie:{Seat}/Klient:{customer}";
    }
}
