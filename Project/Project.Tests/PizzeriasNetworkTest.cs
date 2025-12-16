using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Tests
{
    [Collection("Sequential")]
    public class PizzeriasNetworkTest
    {
        [Fact]
        public void Constructor_InitializesEmptyList()
        {
            var network = new PizzeriasNetwork();

            Assert.NotNull(network.PizzeriasList);
            Assert.Empty(network.PizzeriasList);
        }

        [Fact]
        public void AddPizzeria_ValidPizzeria_AddsToList()
        {
            var network = new PizzeriasNetwork();
            var pizzeria = new Pizzeria("Napoli", "Main St");

            network.AddPizzeria(pizzeria);

            Assert.Single(network.PizzeriasList);
            Assert.Equal("Napoli", network.PizzeriasList[0].Name);
        }

        [Fact]
        public void AddPizzeria_Null_DoesNotAdd()
        {
            var network = new PizzeriasNetwork();

            network.AddPizzeria(null);

            Assert.Empty(network.PizzeriasList);
        }

        [Fact]
        public void AddPizzeria_DuplicateName_DoesNotAddAndPrintsError()
        {
            var network = new PizzeriasNetwork();
            network.AddPizzeria(new Pizzeria("Duplicate", "Address 1"));

            var secondPizzeria = new Pizzeria("Duplicate", "Address 2");

            var originalOut = Console.Out;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    network.AddPizzeria(secondPizzeria);

                    var output = stringWriter.ToString();

                    Assert.Single(network.PizzeriasList);

                    Assert.Equal("Address 1", network.PizzeriasList[0].Address);

                    Assert.Contains("already exists", output);
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RemovePizzeria_ExistingName_RemovesFromList()
        {
            var network = new PizzeriasNetwork();
            network.AddPizzeria(new Pizzeria("To Remove", "Addr"));

            network.RemovePizzeria("To Remove");

            Assert.Empty(network.PizzeriasList);
        }

        [Fact]
        public void RemovePizzeria_NonExistingName_PrintsError()
        {
            var network = new PizzeriasNetwork();
            network.AddPizzeria(new Pizzeria("Keep Me", "Addr"));

            var originalOut = Console.Out;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    network.RemovePizzeria("Ghost Pizzeria");

                    var output = stringWriter.ToString();

                    Assert.Single(network.PizzeriasList);
                    Assert.Contains("Could not find pizzeria", output);
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void GetPizzeria_ReturnsCorrectObjectOrNull()
        {
            var network = new PizzeriasNetwork();
            network.AddPizzeria(new Pizzeria("Find Me", "Addr"));

            var found = network.GetPizzeria("Find Me");
            var notFound = network.GetPizzeria("Invisible");

            Assert.NotNull(found);
            Assert.Equal("Find Me", found.Name);

            Assert.Null(notFound);
        }

        [Fact]
        public void DisplayAll_PrintsNetworkInfo()
        {
            var network = new PizzeriasNetwork();
            network.AddPizzeria(new Pizzeria("Show Me", "Addr"));

            var originalOut = Console.Out;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    network.DisplayAll();

                    var output = stringWriter.ToString();
                    Assert.Contains("NETWORK OVERVIEW", output);
                    Assert.Contains("Show Me", output);
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}
