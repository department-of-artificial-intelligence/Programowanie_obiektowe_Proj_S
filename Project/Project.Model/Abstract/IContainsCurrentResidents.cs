namespace Project.Model.Abstract
{
    public interface IContainsCurrentResidents
    {
        IEnumerable<Resident> Residents { get; }
    }
}
