using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
namespace BLL
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
        public async Task<Service> CreateServiceAsync(string name, string desc, string? type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name of the service cannot be empty");
            }
            var newService = new Service(name, desc, type);
            await _serviceDataLogic.AddAsync(newService);
            await _serviceDataLogic.SaveChangesAsync();
            return newService;
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
