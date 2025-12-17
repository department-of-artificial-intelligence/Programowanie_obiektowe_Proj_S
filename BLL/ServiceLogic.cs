
using RatingSystem.Domain;
using RatingSystem.DAL;
namespace RatingSystem.BLL
{
    public class ServiceLogic : IServiceLogic
    {
        private readonly IServiceDataLogic _serviceDataLogic;
        public ServiceLogic(IServiceDataLogic serviceDataLogic)
        {
            _serviceDataLogic = serviceDataLogic;
        }
        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceDataLogic.GetByIdAsync(serviceId);
        }
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _serviceDataLogic.GetAllAsync();
        }
        public async Task<IEnumerable<Service>> GetAllAsync()
        {

            var services = await _serviceDataLogic.GetAllAsync();
            return services.OrderBy(s => s.Name);
        }
        public async Task<Service> AddServiceAsync(string name, string desc, string? type)
        {
            var existingServices = await _serviceDataLogic.GetAllAsync();
            if (existingServices.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"service already exists");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name of the service cannot be empty");
            }
            var newService = new Service(name, desc, type);
            await _serviceDataLogic.AddAsync(newService);
            await _serviceDataLogic.SaveChangesAsync();
            return newService;
        }
        public async Task<int> GetServiceIdByNameAsync(string serviceName)
        {
            var services= await _serviceDataLogic.GetAllAsync();
            var service = services.FirstOrDefault(s => s.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            if (service == null)
            {
                throw new KeyNotFoundException($"can't find service : {serviceName} ");
            }
            return service.ServiceId;
        }
        public async Task DeleteServiceAsync(int serviceId)
        {
            Service? serviceToDelete = await _serviceDataLogic.GetByIdAsync(serviceId);
            if (serviceToDelete != null)
            {
                await _serviceDataLogic.RemoveAsync(serviceToDelete);
                await _serviceDataLogic.SaveChangesAsync();
            }
        }
                                                                                    
    }
}
