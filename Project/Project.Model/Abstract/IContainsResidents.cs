namespace Project.Model.Abstract
{
    public interface IContainsResidents
    {
        IEnumerable<IResident> AllResidents { get; }
    }
}
