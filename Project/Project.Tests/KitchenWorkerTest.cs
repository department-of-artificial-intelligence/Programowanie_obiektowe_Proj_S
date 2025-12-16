using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [Collection("Sequential")]
    public class KitchenWorkerTest
    {
        [Fact]
        public void KitchenWorkerConstructorTest()
        {
            string firstName = "Gordon";
            string lastName = "Ramsay";
            decimal salary = 5000m;
            string station = "Oven";

            KitchenWorker worker = new KitchenWorker(firstName, lastName, salary, station);

            Assert.Equal(firstName, worker.FirstName);
            Assert.Equal(lastName, worker.LastName);
            Assert.Equal(salary, worker.Salary);
            Assert.Equal(station, worker.Station);

            Assert.Equal("Kitcher Stuff", worker.Position);
        }

        [Fact]
        public void PrepareFoodTest()
        {
            KitchenWorker worker = new KitchenWorker("Gordon", "Test", 1000m, "Grill");

            var output = new StringWriter();
            Console.SetOut(output);

            worker.PrepareFood();

            var result = output.ToString();
            Assert.Contains("Gordon is preparing food at the Grill", result);

            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
    }
}
