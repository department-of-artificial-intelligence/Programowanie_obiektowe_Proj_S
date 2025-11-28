using Project.Model;
using System.Diagnostics.CodeAnalysis;

using GeneratedReport = Project.Reports.Generators.RevenueInTimeRangeReportGenerator.RevenueInTimeRangeReport;

namespace Project.Reports.Generators
{
    public class RevenueInTimeRangeReportGenerator : IReportGenerator<GeneratedReport, Hotel>
    {
        public required DateTime StartDate { get; init; }

        public required DateTime EndDate { get; init; }

        public RevenueInTimeRangeReportGenerator() { }

        [SetsRequiredMembers]
        public RevenueInTimeRangeReportGenerator(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        private long GetDay(DateTime dateTime)
        {
            return (int) (dateTime.Day + (dateTime.Year * 365.25));
        }

        private decimal CalculateRevenueInRoom(HotelRoom room)
        {
            var allRanges = room.HistoricResidents.Select(x => (x.ResidentFrom, x.ResidentTo));

            var aggregateResult = allRanges.Aggregate((Revenue: 0, LastCalculatedDateTime: DateTime.MinValue), (acc, range) =>
            {
                if (range.ResidentTo > this.EndDate || range.ResidentTo < acc.LastCalculatedDateTime)
                {
                    return acc;
                }
                
                if (range.ResidentFrom < this.StartDate)
                {
                    range.ResidentFrom = this.StartDate;
                }

                if (range.ResidentFrom < acc.LastCalculatedDateTime)
                {
                    range.ResidentFrom = acc.LastCalculatedDateTime;
                }

                if (range.ResidentTo > this.EndDate)
                {
                    range.ResidentTo = this.EndDate;
                }

                var startDay = this.GetDay(range.ResidentFrom);
                var endDay = this.GetDay(range.ResidentTo);

                return ((int) (endDay - startDay), range.ResidentTo);
            });

            return aggregateResult.Revenue * room.PricePerDay;
        }

        public Report<GeneratedReport> GenerateReport(Hotel entity)
        {
            var revenue = entity.Rooms.Select(CalculateRevenueInRoom).Sum();
            
            var reportDetails = new RevenueInTimeRangeReport()
            {
                StartDate = StartDate,
                EndDate = EndDate,
                Revenue = revenue
            };

            return new Report<GeneratedReport>(reportDetails);
        }

        public record RevenueInTimeRangeReport
        {
            public required DateTime StartDate { get; init; }

            public required DateTime EndDate { get; init; }

            public required decimal Revenue { get; init; }
        }
    }
}
