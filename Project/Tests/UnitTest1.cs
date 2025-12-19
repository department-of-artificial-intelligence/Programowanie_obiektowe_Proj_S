using Project.Model;

namespace Project.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PersonAgeTest()
        {
            Person p1 = new Person("Jan","Kowalski",12);

            Assert.False(p1.Age<18,"Osoba niepełnoletnia");
        }
        /*public void TicketSeatNumberTest()
        {
            Ticket t1 = new Ticket();
            t1.Seat_Number = "as";

            Assert.False(t1.Seat_Number < 0, $"Niepoprawny numer miejsca: {t1.Seat_Number}");
        }*/
        [Fact]
        public void ConcertTicketsOverLimit()
        {
            Concert c1 = new Concert();
            Venue v1 = new Venue();
            v1.Capacity = 130;
            //v1.FloorCapacity = 100;
            //v1.SeatsCapacity = 30;
            c1.Venue = v1;
            c1.TicketsSold = 150;
            //int _capacity = v1.SeatsCapacity + v1.SeatsCapacity;

            Assert.False(v1.Capacity < c1.TicketsSold, "Sprzedano więcej biletów niż jest dostępnych miejsc");
        }
        [Fact]
        public void VenueTest()
        {
            var v1 = new Venue();
            DateTime date = DateTime.Now;
            Concert c1 = new Concert();
            Concert c2 = new Concert();
            c1.Date = date;
            c1 .Venue = v1;
            c2.Date = date;
            c2.Venue = v1;

            Assert.False(c1.Date == c2.Date && c1.Venue == c2.Venue, "Dwa koncerty w tym samym miejscu");
           
        }

        [Fact]
        public void TicketPriceNotNegative()
        {
            Ticket t1 = new Ticket();

            t1.Price = -30.20;

            Assert.False(t1.Price < 0, "Ujemna cena biletu");
        }
    }

}