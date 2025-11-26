namespace RatingSystem.Domain;

public class Rating
{
    private int _id;
    private int _serviceId;
    private int _value;
    private string _comment;

    public Service Service
    {
        get;
        set;
    }
    public int Id
    {
        get => _id;
        set => _id = value;
    }

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

    Rating(int id, int serviceId, int value, string comment)
    {
        _id = id;
        _serviceId = serviceId;
        _value = value;
        _comment = comment;
    }
    Rating():this(0, 0, 0, string.Empty){}
    
}