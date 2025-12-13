namespace RatingSystem.Domain;

public class Service
{
    private int _id;
    private string _name;
    private string _description;
    private int _numOfGrades;
    private string? _serviceType;
    public Rating rating {  get; set; }
    public float Quality { get; set; }
    public float Price { get; set; }
    public float Overall {  get; set; }
    public int  Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int NumOfGrades{get => _numOfGrades;}
    public string Description { get => _description; set => _description = value; }

    public string? ServiceType
    {
        get => _serviceType;
    }

    public Service(int id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }

    public Service() : this(0, "", ""){}
    
}