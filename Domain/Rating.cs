using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RatingSystem.Domain;

public class Rating
{
   
   

    public int RatingId { get; set; }
    public int UserId {  get; set; }
    public int ServiceId {  get; set; }
    public int Value { get; set; }
    public string Comment { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public User? User { get; set;  }
    public Service? Service { get; set; }

    public Rating(int userId, int serviceId, int value, string comment)
    {
        UserId = userId;
        ServiceId = serviceId;
        Value = value;
        Comment = comment;
        Created = DateTime.Now;
    }
    public Rating():this(0, 0, 0, string.Empty){}
    
}