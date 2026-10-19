

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

        public void AddCity(City city)
        {
            Cities.Add(city);
        }

        public List<Restaurant>? FindRestaurants(string cityName)
        {
            City? foundedCity = Cities.Find(city => city.Name == cityName);

            if (foundedCity != null)
            {
                return foundedCity.Restaurants;
            }
            else
            {
                return null;
            }
        }

        public void GetAllRestaurants()
        {
            if (Cities.Count == 0)
            {
                Console.WriteLine("No cities with resturant");
            }
            else
            {
                foreach (City city in Cities)
                {
                    Console.WriteLine($"{city.Name}:");
                    if (city.Restaurants.Count == 0)
                    {
                        Console.WriteLine("No resturants in city");
                    }
                    else
                    {
                        foreach (Restaurant restaurant in city.Restaurants)
                        {
                            Console.WriteLine($"{restaurant.Name}");
                        }
                    }
                }
            }
        }
    }
}