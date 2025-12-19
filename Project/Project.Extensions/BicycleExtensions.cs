using System;
using Project.Model;

namespace Project.Extensions
{
    public static class BicycleExtensions
    {
        public static string ToPriceString(this Bicycle bike)
        {
            return $"{bike.Price:0.00} PLN";
        }
    }
}
