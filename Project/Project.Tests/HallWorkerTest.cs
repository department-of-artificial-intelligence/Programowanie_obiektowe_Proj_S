using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [Collection("Sequential")]
    public class HallWorkerTest
    {
        [Fact]
        public void HallWorkerConstructorTest()
        {
            string firstName = "Luigi";
            string lastName = "Bros";
            decimal salary = 3000m;

            HallWorker worker = new HallWorker(firstName, lastName, salary);

            Assert.Equal(firstName, worker.FirstName);
            Assert.Equal(lastName, worker.LastName);
            Assert.Equal(salary, worker.Salary);
            Assert.Equal("Hall Staff", worker.Position);
            Assert.Equal(0, worker.AssignedTables);
        }

        [Fact]
        public void AssignedTablesTest()
        {
            HallWorker worker = new HallWorker("Test", "User", 100m);

            worker.AssignedTables = 5;

            Assert.Equal(5, worker.AssignedTables);
        }

        [Fact]
        public void ServeClientTest()
        {
            HallWorker worker = new HallWorker("Mario", "Test", 100m);
            Client client = new Client(1, "Peach", "Princess", "123");

            var output = new StringWriter();
            Console.SetOut(output);

            worker.ServeClient(client);

            var result = output.ToString();
            Assert.Contains("Mario is serving Peach Princess", result);

            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        [Fact]
        public void CleanTableTest()
        {
            HallWorker worker = new HallWorker("Luigi", "Test", 100m);
            int tableNum = 10;

            var output = new StringWriter();
            Console.SetOut(output);

            worker.CleanTable(tableNum);

            var result = output.ToString();
            Assert.Contains("Luigi is cleaning table 10.", result);

            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
    }
}
