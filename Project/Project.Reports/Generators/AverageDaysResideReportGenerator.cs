using Project.Model.Abstract;

using GeneratedReport = Project.Reports.Generators.AverageDaysResideReportGenerator.AverageDaysResideReportDetails;

namespace Project.Reports.Generators
{
    public class AverageDaysResideReportGenerator : IReportGenerator<GeneratedReport, IContainsCurrentResidents>
    {
        public Report<GeneratedReport> GenerateReport(IContainsCurrentResidents entity)
        {
            var averageAge = entity.Residents
                .Select(x => (DateTime.Now - x.ResidentFrom).TotalDays)
                .Average();

            var reportDetails = new GeneratedReport()
            {
                AverageDaysReside = (int)Math.Floor(averageAge)
            };
            
            return new Report<GeneratedReport>(reportDetails);
        }

        public record AverageDaysResideReportDetails
        {
            public required int AverageDaysReside { get; init; }
        }
    }
}
