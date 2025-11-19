namespace RatingSystem.Domain;

public class Service
{
    private int _id;
    private string _name;
    private string _description;
    public int  Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }

    public Service(int id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }

    public Service() : this(0, "", ""){}
    
}