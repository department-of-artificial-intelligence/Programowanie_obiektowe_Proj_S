using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model.Utils;
using Project.Reports.Generators;

namespace Project.ConsoleApp
{
    internal class Program
    {
        private static readonly HotelGenerator s_hotelGenerator = new HotelGenerator(0xBEEF /* cats love beef */);
        
        private static void Main()
        {
            var dbContext = new ApplicationDbContextFactory().CreateDbContext([]);

            dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();

            var isHotelExists = dbContext.Hotels.Any();

            if (!isHotelExists)
            {
                dbContext.Hotels.Add(s_hotelGenerator.GenerateHotel());
                dbContext.SaveChanges();
            }
            
            var hotel = dbContext.Hotels
                .Include(x => x.Manager)
                .Include(x => x.Rooms)
                    .ThenInclude(x => x.Residents)
                        .ThenInclude(x => x.Person)
                .Include(x => x.Rooms)
                    .ThenInclude(x => x.HistoricResidents)
                        .ThenInclude(x => x.Person)
                .First();

            // id info extraction
            Console.WriteLine(hotel);
            Console.WriteLine(UlongIdGenerator.ExtractInfo(hotel.Id));
            
            // reports generation
            var ageReportGenerator = new AverageAgeReportGenerator();
            var ageReport = ageReportGenerator.GenerateReport(hotel);

            var resideReportGenerator = new AverageDaysResideReportGenerator();
            var resideReport = resideReportGenerator.GenerateReport(hotel);
            
            var peopleWithAgeReportGenerator = new PeopleWithAgeMoreThanProvidedReportGenerator(25);
            var peopleWithAgeReport = peopleWithAgeReportGenerator.GenerateReport(hotel);

            var revenueReportGenerator = new RevenueInTimeRangeReportGenerator(DateTime.Now.AddDays(-1000), DateTime.Now);
            var revenueReport = revenueReportGenerator.GenerateReport(hotel);
            
            Console.WriteLine(ageReport);
            Console.WriteLine(resideReport);
            Console.WriteLine(revenueReport);
            
            peopleWithAgeReport.Details.People.ForEach(Console.WriteLine);
            hotel.Rooms.ToList().ForEach(Console.WriteLine);
        }
    }
}
