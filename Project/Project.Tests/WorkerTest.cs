using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    public class WorkerTest
    {
        [Fact]
        public void WorkerCreationTest()
        {
            string fn = "Mario";
            string ln = "Rossi";
            decimal salary = 3000m;
            string pos = "Chef";

            Worker w = new Worker(fn, ln, salary, pos);

            Assert.Equal("Mario", w.FirstName);
            Assert.Equal("Rossi", w.LastName);
            Assert.Equal(3000m, w.Salary);
            Assert.Equal("Chef", w.Position);
        }

        [Fact]
        public void GetInfoTest()
        {
            Worker w = new Worker("Luigi", "Verdi", 2500m, "Waiter");

            string info = w.GetInfo();

            Assert.Contains("Luigi", info);
            Assert.Contains("2500", info);
            Assert.Contains("Waiter", info);
        }
    }
}
