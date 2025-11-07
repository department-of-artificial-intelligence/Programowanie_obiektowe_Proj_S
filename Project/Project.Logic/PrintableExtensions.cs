using System.Linq;
using Project.Model;

namespace Project.Logic
{
    public static class DataDisplay
    {
        public static IEnumerable<Vehicle> FilterByAvailabilty(this IEnumerable<Vehicle> obj)
        {
            return obj.Where(v => v.IsAvailable);
        }

        public static IEnumerable<Vehicle> SortByMileage(this IEnumerable<Vehicle> obj)
        {
            return obj.OrderBy(v => v.Mileage);
        }

        public static IEnumerable<Vehicle> SortByProductionYearDesc(this IEnumerable<Vehicle> obj)
        {
            return obj.OrderByDescending(v => v.ProductionYear);
        }

        public static IEnumerable<T> GroupByAvailbility(thus IEnumerable<T> obj)
        {

        }

        //====================== Driver ==============================
    }
}

/* filtrowanie - jakie zlecenia ma dany kierowca, które auto jest do danych zleceń
 * statystki - które auta są najczęściej wypożyczne
 * 

wyr. lambda statystki, sortowanie - jako lista, linq
project.logic i tam logika reprezentacja danych
IPrintable
*/