using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    public class ClientTest
    {
        [Fact]
        public void Client_Constructor_SetsPropertiesCorrectly()
        {
            int expectedId = 101;
            string expectedFirstName = "Mads";
            string expectedLastName = "Mikkelsen";
            string expectedPhone = "555-1234";

            Client client = new Client(expectedId, expectedFirstName, expectedLastName, expectedPhone);

            Assert.Equal(expectedId, client.Id);
            Assert.Equal(expectedFirstName, client.FirstName);
            Assert.Equal(expectedLastName, client.LastName);
            Assert.Equal(expectedPhone, client.PhoneNumber);
        }

        [Fact]
        public void Client_DefaultConstructor_CreatesEmptyClient()
        {
            Client client = new Client();

            Assert.Equal(0, client.Id);
            Assert.Equal(string.Empty, client.FirstName);
            Assert.Equal(string.Empty, client.LastName);
            Assert.Equal(string.Empty, client.PhoneNumber);
        }

        [Fact]
        public void GetFullNameTest()
        {
            Client client = new Client(1, "Anna", "Smith", "123");

            string fullName = client.GetFullName();

            Assert.Equal("Anna Smith", fullName);
        }

        [Fact]
        public void GetInfoTest()
        {
            Client client = new Client(5, "Tom", "Hanks", "999-888");

            string info = client.GetInfo();

            Assert.Contains("---- Client ----", info);
            Assert.Contains("ID: 5", info);
            Assert.Contains("Name: Tom Hanks", info);
            Assert.Contains("Phone: 999-888", info);
        }
    }
}
