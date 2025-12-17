using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.DAL;
using RatingSystem.Domain;
namespace RatingSystem.BLL
{
    public class RatingAnalytics
    {
        private readonly IRatingDataLogic _ratingDataLogic;

        public RatingAnalytics(IRatingDataLogic ratingDataLogic)
        {
            _ratingDataLogic = ratingDataLogic;
        }

        //  Complex Statistics 
        public async Task<string> GetComplexStatsReportAsync()
        {
            var ratings = await _ratingDataLogic.GetAllAsync();

            var stats = ratings
                .GroupBy(r => r.Service?.Name ?? "Unknown")
                .Select(g => new {
                    Name = g.Key,
                    Avg = g.Average(r => r.Value),
                    Count = g.Count(),
                    Max = g.Max(r => r.Value)
                })
                .OrderByDescending(x => x.Avg)
                .Select(x => $"{x.Name}: Avg Score {x.Avg:F1} | Max: {x.Max} | Total: {x.Count}")
                .ToList();

            string header = "--- Service Performance Report ---\n";
            return header + string.Join("\n", stats);
        }

        //  Top 3 Services 
        public async Task<string> GetTop3ServicesReportAsync()
        {
            var ratings = await _ratingDataLogic.GetAllAsync();

            var top3 = ratings
                .GroupBy(r => r.ServiceId)
                .Select(g => new {
                    Name = g.FirstOrDefault()?.Service?.Name ?? "Unknown",
                    Avg = g.Average(r => r.Value),
                    Votes = g.Count()
                })
                .OrderByDescending(x => x.Avg)
                .Take(3)
                .Select((x, index) => $"{index + 1}. {x.Name} - Rating: {x.Avg:F1} ({x.Votes} votes)")
                .ToList();

            string header = "--- TOP 3 SERVICES ---\n";
            return top3.Any()
                ? header + string.Join("\n", top3)
                : header + "No data available.";
        }
    }
}
