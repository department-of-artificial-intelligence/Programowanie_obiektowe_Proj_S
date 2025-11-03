namespace Project.Interfaces
{
    public interface IListManageable<T>
    {
        bool AddItem(T item);
        bool RemoveItem(T item);
        string GetItemsAsString();
        IReadOnlyList<T> Items { get; }
    }
}
