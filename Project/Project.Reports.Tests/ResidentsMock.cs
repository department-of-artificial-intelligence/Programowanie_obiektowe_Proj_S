using Project.Model;
using Project.Model.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Project.Reports.Tests
{
    internal class ResidentsMock : IContainsResidents, IContainsCurrentResidents
    {
        public required List<Resident> MockedResidents { get; init; }

        public List<IResident> AllResidents => this.MockedResidents.Cast<IResident>().ToList();

        public List<Resident> Residents => this.MockedResidents;

        public ResidentsMock() { }

        [SetsRequiredMembers]
        public ResidentsMock(List<Resident> mockedResidents)
        {
            MockedResidents = mockedResidents;
        }
    }
}
