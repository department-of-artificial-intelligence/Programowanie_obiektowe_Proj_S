using Project.Model.Abstract;

using GeneratedReport = Project.Reports.Generators.AverageAgeReportGenerator.AverageAgeReportDetails;

namespace Project.Reports.Generators
{
    public class AverageAgeReportGenerator : IReportGenerator<IContainsResidents, GeneratedReport>
    {
        public Report<GeneratedReport> GenerateReport(IContainsResidents entity)
        {
            var averageAge = entity.AllResidents
                .Select(x => (DateTime.Now - x.Person.DateOfBirth).TotalDays)
                .Average();
            
            return new Report<GeneratedReport>(new GeneratedReport()
            {
                AverageAge = (int) Math.Floor(averageAge / 365.25)
            });
        }

        public record AverageAgeReportDetails
        {
            public required int AverageAge { get; init; }
        }
    }
}
