using Project.Model;
using Project.Reports.Generators;

namespace Project.ConsoleApp
{
    internal class Program
    {
        private static Hotel s_hotel = new Hotel()
        {
            Address = "12 Kitten st. Catshington DC",
            Manager = new Manager() { Person = new Person("Senior", "Kitten", DateTime.Now) },
            Name = "Cat Hotel",
            Rooms = new List<HotelRoom>()
                {
                    new HotelRoom()
                    {
                        Floor = 1,
                        HistoricResidents = new List<RoomHistoricResident>()
                        {

                        },
                        Number = 1,
                        Residents = new List<Resident>()
                        {
                            new Resident()
                            {
                                Person = new Person("Cat", "(just a cat)", new DateTime(1999, 1, 1)), // in this (definitely better) universe cats live more than 20 years
                                ResidentFrom = new DateTime(2010, 1, 1)
                            },
                            new Resident()
                            {
                                Person = new Person("Cat 2", "(just a cat)", new DateTime(2001, 1, 1)),
                                ResidentFrom = new DateTime(2020, 1, 1)
                            }
                        }
                    }
                }
        };

        private static void Main()
        {
            var ageReportGenerator = new AverageAgeReportGenerator();
            var ageReport = ageReportGenerator.GenerateReport(s_hotel);

            var resideReportGenerator = new AverageDaysResideReportGenerator();
            var resideReport = resideReportGenerator.GenerateReport(s_hotel);

            Console.WriteLine(ageReport);
            Console.WriteLine(resideReport);
        }
    }
}
