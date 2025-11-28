using System.Net;

namespace Project.Model
{
    public class Station
    {
        public string Location { get; set; }
        public int Amount { get; set; }
        private List<Bicycle> bicycles = new List<Bicycle>();
        public int BicycleCount { get { return bicycles.Count; } }

        public Station( string location, int amount)
        {
            
            Location = location;
            Amount = amount;
        }

        public bool AddBicycle(Bicycle b)
        {

            if (b == null || bicycles.Count >= this.Amount) return false;
            foreach (Bicycle bic in bicycles)
            {
                if (b.Id == bic.Id) return false;
            }
            bicycles.Add(b);
            b.Return(this);
            return true;
        }

        public void RemoveBicycle(Bicycle b)
        {
            bicycles.Remove(b);
        }

        public override string ToString()
        {
            string s = string.Format("Station {0} ({1}/{2} bicycles):", Location, bicycles.Count, Amount);
            foreach (Bicycle bic in bicycles)
            {
                s += "\n- " + bic.ToString();
            }
            return s;
        }
    }
}
