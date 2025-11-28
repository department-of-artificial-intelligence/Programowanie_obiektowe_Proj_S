using Project.Model;
using Project.Model.Utils;
using Project.Reports.Generators;

namespace Project.ConsoleApp
{
    internal class Program
    {
        private static readonly HotelGenerator s_hotelGenerator = new HotelGenerator(0xBEEF /* cats love beef */);
        private static readonly Hotel s_hotel = s_hotelGenerator.GenerateHotel();
        
        private static void Main()
        {
            // id info extraction
            Console.WriteLine(s_hotel);
            Console.WriteLine(UlongIdGenerator.ExtractInfo(s_hotel.Id));
            
            // reports generation
            var ageReportGenerator = new AverageAgeReportGenerator();
            var ageReport = ageReportGenerator.GenerateReport(s_hotel);

            var resideReportGenerator = new AverageDaysResideReportGenerator();
            var resideReport = resideReportGenerator.GenerateReport(s_hotel);
            
            var peopleWithAgeReportGenerator = new PeopleWithAgeMoreThanProvidedReportGenerator(25);
            var peopleWithAgeReport = peopleWithAgeReportGenerator.GenerateReport(s_hotel);

            var revenueReportGenerator = new RevenueInTimeRangeReportGenerator(DateTime.Now.AddDays(-1000), DateTime.Now);
            var revenueReport = revenueReportGenerator.GenerateReport(s_hotel);
            
            Console.WriteLine(ageReport);
            Console.WriteLine(resideReport);
            Console.WriteLine(revenueReport);
            
            peopleWithAgeReport.Details.People.ForEach(Console.WriteLine);
            s_hotel.Rooms.ToList().ForEach(Console.WriteLine);
        }
    }
}
