using Project.Model;
using Project.Model.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Project.Reports.Tests
{
    internal class ResidentsMock : IContainsResidents, IContainsCurrentResidents
    {
        public required List<Resident> MockedResidents { get; init; }

        public IEnumerable<IResident> AllResidents { get => this.MockedResidents; }

        public IEnumerable<Resident> Residents { get => this.MockedResidents; }
    
        public ResidentsMock() { }

        [SetsRequiredMembers]
        public ResidentsMock(List<Resident> mockedResidents)
        {
            MockedResidents = mockedResidents;
        }
    }
}
