using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Model;

public static class PerformanceGroupingExtension
{
    public class PerformanceStatusReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ScheduledCount { get; set; }
        public int CanceledCount { get; set; }
        public int FinishedCount { get; set; }
        public int InProgressCount { get; set; }
        public int TotalCount { get; set; }

        public override string ToString()
        {
            return $"Podsumowanie dla {Year}-{Month:D2}: " +
                   $"Zaplanowane: {ScheduledCount}, " +
                   $"Odwołane: {CanceledCount}, " +
                   $"Zakończone: {FinishedCount}, " +
                   $"W trakcie: {InProgressCount}, " +
                   $"Razem: {TotalCount}";
        }
    }

    public static List<PerformanceStatusReport> GetMonthlyStatusReportsByYear(this IList<Performance> performances, int year)
    {
        return performances
            .Where(p => p.StartTime.Year == year)
            .GroupBy(p => p.StartTime.Month)
            .Select(g => new PerformanceStatusReport
            {
                Year = year,
                Month = g.Key,
                ScheduledCount = g.Count(p => p.Status == PerformanceStatus.Scheduled),
                CanceledCount = g.Count(p => p.Status == PerformanceStatus.Canceled),
                FinishedCount = g.Count(p => p.Status == PerformanceStatus.Finished),
                InProgressCount = g.Count(p => p.Status == PerformanceStatus.InProgress),
                TotalCount = g.Count()
            })
            .OrderBy(r => r.Month)
            .ToList();
    }
}
