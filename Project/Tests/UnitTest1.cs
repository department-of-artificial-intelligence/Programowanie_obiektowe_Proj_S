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
        public void TicketSeatNumberTest()
        {
            Ticket t1 = new Ticket();
            t1.Seat_Number = -1;

            Assert.False(t1.Seat_Number < 0, $"Niepoprawny numer miejsca: {t1.Seat_Number}");
        }
    }
}