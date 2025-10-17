namespace Project.Model
{
    internal class Station
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int Amount { get; set; }
        private List<Bicycle> bicycles = new List<Bicycle>();

        public Station(int id, string location, int amount)
        {
            Id = id;
            Location = location;
            Amount = amount;
        }

        public void AddBicycle(Bicycle b)
        {
            bicycles.Add(b);
            b.CurrentStation = this;
        }

        public void RemoveBicycle(Bicycle b)
        {
            bicycles.Remove(b);
        }

    }
}
