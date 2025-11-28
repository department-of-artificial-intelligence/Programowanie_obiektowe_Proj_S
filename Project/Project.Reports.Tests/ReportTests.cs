using Project.Model;
using Project.Reports.Generators;

namespace Project.Reports.Tests
{
    public class ReportTests
    {
        private ResidentsMock _testData = new ResidentsMock()
        {
            MockedResidents = new List<Resident>()
            {
                new Resident()
                {
                    Person = new Person()
                    {
                        DateOfBirth = DateTime.Now.AddYears(-20),
                        FirstName = "Person", LastName = "1"
                    },
                    ResidentFrom = DateTime.Now.AddDays(-100)
                },
                new Resident()
                {
                    Person = new Person()
                    {
                        DateOfBirth = DateTime.Now.AddYears(-30),
                        FirstName = "Person", LastName = "2"
                    },
                    ResidentFrom = DateTime.Now.AddDays(-200)
                }
            }
        };

        [Fact]
        public void AverageAgeReportGenerator()
        {
            var reportGenerator = new AverageAgeReportGenerator();
            var report = reportGenerator.GenerateReport(this._testData);

            Assert.Equal(25, report.Details.AverageAge);
        }

        [Fact]
        public void AverageDaysResideReportGenerator()
        {
            var reportGenerator = new AverageDaysResideReportGenerator();
            var report = reportGenerator.GenerateReport(this._testData);

            Assert.Equal(150, report.Details.AverageDaysReside);
        }

        [Fact]
        public void AverageWithAgeMoreThanProvidedReportGeneratorTest()
        {
            var reportGenerator = new PeopleWithAgeMoreThanProvidedReportGenerator(25);
            var report = reportGenerator.GenerateReport(this._testData);

            Assert.Single(report.Details.People);
            Assert.Equal(this._testData.AllResidents.Last().Person, report.Details.People.First());
        }
    }
}
