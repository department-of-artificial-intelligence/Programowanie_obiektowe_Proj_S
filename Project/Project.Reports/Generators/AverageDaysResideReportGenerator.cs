using Project.Model.Abstract;

using GeneratedReport = Project.Reports.Generators.AverageDaysResideReportGenerator.AverageDaysResideReportDetails;

namespace Project.Reports.Generators
{
    public class AverageDaysResideReportGenerator : IReportGenerator<IContainsCurrentResidents, GeneratedReport>
    {
        public Report<GeneratedReport> GenerateReport(IContainsCurrentResidents entity)
        {
            var averageAge = entity.Residents
                .Select(x => (DateTime.Now - x.ResidentFrom).TotalDays)
                .Average();

            return new Report<GeneratedReport>(new GeneratedReport()
            {
                AverageDaysReside = (int) Math.Floor(averageAge)
            });
        }

        public record AverageDaysResideReportDetails
        {
            public required int AverageDaysReside { get; init; }
        }
    }
}
