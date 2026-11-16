namespace Shelter.Model.DataModels
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<Sector> Sectors { get; set; }
        public List<Animal> Animals { get; set; }
        public List<Employee> Employees { get; set; }
        public Shelter(int id, string address, List<Sector> sectors, List<Animal> animals, List<Employee> employees)
        {
            Id = id;
            Address = address;
            Sectors = sectors;
            Animals = animals;
            Employees = employees;
        }
    }
}
