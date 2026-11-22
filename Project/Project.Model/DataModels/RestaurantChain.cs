

namespace Project.Model
{
    public class RestaurantChain
    {
        public string Name { get; set; }
        public List<City> Cities { get; }

        public RestaurantChain() : this(string.Empty) { }

        public RestaurantChain(string name)
        {
            Name = name;
            Cities = new List<City>();
        }
        }
    }
