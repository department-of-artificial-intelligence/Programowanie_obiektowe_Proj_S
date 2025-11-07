using Project.Model;
using Project.Model.Abstract;
using System.Diagnostics.CodeAnalysis;
using GeneratedReport = Project.Reports.Generators.PeopleWithAgeMoreThanProvidedReportGenerator.PeopleWithAgeMoreThanProvidedReport;

namespace Project.Reports.Generators
{
    // short version of the old name
    public class PeopleWithAgeMoreThanProvidedReportGenerator : IReportGenerator<IContainsResidents, GeneratedReport>
    {
        public required int Age { get; init; }

        public PeopleWithAgeMoreThanProvidedReportGenerator() { }

        [SetsRequiredMembers]
        public PeopleWithAgeMoreThanProvidedReportGenerator(int age)
        {
            this.Age = age;
        }

        public Report<GeneratedReport> GenerateReport(IContainsResidents entity)
        {
            //var people = entity.AllResidents
            //    .Select(x => x.Person)
            //    .Where(x => xs)

            return new Report<GeneratedReport>(new PeopleWithAgeMoreThanProvidedReport()
            {
                People = []
            });
        }

        public record PeopleWithAgeMoreThanProvidedReport
        {
            public List<Person> People { get; init; }
        }
    }
}
