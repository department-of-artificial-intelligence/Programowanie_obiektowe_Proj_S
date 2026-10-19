using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class City
    {
        public string Name { get; set; }
        public List<Restaurant> Restaurants { get; }

        public City() : this(string.Empty) { }

        public City(string name)
        {
            Name = name;
            Restaurants = new List<Restaurant>();
        }

        public City(string name, List<Restaurant> restaurants)
        {
            Name = name;
            Restaurants = restaurants;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            Restaurants.Add(restaurant);
        }
    }
}