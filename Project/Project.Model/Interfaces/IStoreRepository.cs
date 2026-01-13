using Project.Model.Stores;

public interface IStoreRepository
{
    List<Store> GetAll();
    Store GetById(int id);
    bool Add(Store store);
    bool Update(Store store);
    bool Remove(int id);
}