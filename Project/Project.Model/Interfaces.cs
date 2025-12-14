namespace Project.Model;

public interface IPlayManager
{
    bool AddPlay(Play play);
    bool RemovePlay(Play play);
    bool RemoveAllPlays();
    string GetPlaysString();
}

public interface ITicketTransactions
{
    bool BuyTicket(Ticket ticket);
    bool RefundTicket(Ticket ticket);
    bool ReserveTicket(Ticket ticket);
    bool CancelReservation(Ticket ticket);
    bool BuyAllReserved();
    bool CancelAllReserved();
    bool RefundAllBought();
}
