using RatingSystem.Domain;


namespace RatingSystem.BLL
{
    public  interface IServiceLogic
    {
        Task<Service?> GetServiceByIdAsync(int serviceId);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> AddServiceAsync(string name, string description, string? type);
        Task DeleteServiceAsync(int serviceId);
        Task<int> GetServiceIdByNameAsync(string serviceName);
        Task<IEnumerable<Service>> GetAllAsync();
    }
}
