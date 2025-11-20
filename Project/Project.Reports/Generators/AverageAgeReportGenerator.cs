using Project.Extensions;
using Project.Model.Abstract;

using GeneratedReport = Project.Reports.Generators.AverageAgeReportGenerator.AverageAgeReportDetails;

namespace Project.Reports.Generators
{
    public class AverageAgeReportGenerator : IReportGenerator<GeneratedReport, IContainsResidents>
    {
        public Report<GeneratedReport> GenerateReport(IContainsResidents entity)
        {
            var averageAge = entity.AllResidents
                .Select(x => x.Person.DateOfBirth.YearsBetween(DateTime.Now))
                .Average();

            var reportDetails = new GeneratedReport()
            {
                AverageAge = (int) averageAge
            };
            
            return new Report<GeneratedReport>(reportDetails);
        }

        public record AverageAgeReportDetails
        {
            public required int AverageAge { get; init; }
        }
    }
}
