namespace RatingSystem.Domain;

public class Rating
{
    private int _ratingId;
    private int _serviceId;

    private int _value;
    private string _comment;



    public Service? Service
    {
        get;
        set;
    }
    public  int UserId{get;set;}
    public int RatingId
    {
        get => _ratingId;
        set => _ratingId = value;
    }
    public DateTime Date { get; set; }
    public int ServiceId
    {
        get => _serviceId;
        set => _serviceId = value;
    }

    public int Value
    {
        get => _value;
        set => _value = value;
    }

    public string Comment
    {
        get => _comment;
        set => _comment = value;
    }

     public Rating(int id, int serviceId, int value, string comment)
    {
        _ = id;
        _serviceId = serviceId;
        _value = value;
        _comment = comment;
    }
    public Rating():this(0, 0, 0, string.Empty){}
    
}