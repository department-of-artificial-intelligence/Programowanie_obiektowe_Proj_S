namespace Project.Model;

public interface IPlayManager
{
    bool AddPlay(Play play);
    bool RemovePlay(Play play);
    void RemoveAllPlays();
    string GetPlaysString();
}

public interface ITicketTransactions
{
    bool BuyTicket(Ticket ticket);
    bool ReserveTicket(Ticket ticket);
    bool CancelReservation(Ticket ticket);

    void BuyAllReserved();
    void CancelAllReserved();
}
