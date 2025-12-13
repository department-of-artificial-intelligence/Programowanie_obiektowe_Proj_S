using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RatingSystem.Domain;
using RatingSystem.DAL;
namespace RatingSystem.BLL

{
    public  class ServiceLogic: IService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceLogic(IServiceRepository sR)
        {
            _serviceRepository = sR; 
        }
        public async Task AddServiceAsync(Service service)
        {
            if (string.IsNullOrWhiteSpace(service.Name))
            {
                throw new ArgumentException(" name line cannot be empty");
            }
            await _serviceRepository.AddAsync(service);

        }
        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepository.GetByIdAsync(serviceId);
        }
        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _serviceRepository.GetAllAsync(); 
        }
    }
}
