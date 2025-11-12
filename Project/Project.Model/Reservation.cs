using System.Collections.Generic;
namespace RestaurantNetwork.Model;

public class Reservation
{

    public string CustomerName { get; set; }
    public int NumberOfPeople { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Date { get; set; }
    public TimeOnly Time { get; set; }

    public override string ToString()
    {
        return $"{CustomerName} - {NumberOfPeople} osób, {Date:g}";
    }

}
