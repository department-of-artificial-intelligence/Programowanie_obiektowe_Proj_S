using Microsoft.Extensions.DependencyInjection;
using Project.Model;
using Project.Model.DataModels;
using Project.Services.Interfaces;
using Project.Services.Services;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
            .AddTransient<ICityService, CityService>()
            .AddTransient<IDishIngridientService, DishIngridientService>()
            .AddTransient<IDishService, DishService>()
            .AddTransient<IEmployeeService, EmployeeService>()
            .AddTransient<IIngridientService, IngridientService>()
            .AddTransient<IOrderService, OrderServices>()
            .AddTransient<IRestaurantChainService, RestaurantChainService>()
            .AddTransient<IRestaurantService, RestaurantService>()
            .AddTransient<IStockItemService, StockItemService>()
            .AddTransient<IStorageService, StorageServices>()
            .BuildServiceProvider();


            IRestaurantChainService chainService =
                services.GetRequiredService<IRestaurantChainService>();

            RestaurantChain chain = new("My Restaurant Chain");

            City warsaw = new("Warszawa");
            City krakow = new("Kraków");


            chainService.AddCity(chain, warsaw);
            chainService.AddCity(chain, krakow);

            foreach (City city in chainService.GetAllCities(chain))
            {
                Console.WriteLine(city.Name);
            }

        }
    }
}