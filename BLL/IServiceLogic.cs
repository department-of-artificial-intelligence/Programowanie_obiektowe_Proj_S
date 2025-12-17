using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public  interface IServiceLogic
    {
        Task<Service?> GetServiceByIdAsync(int serviceId);
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> CreateServiceAsync(string name, string description, string? type);
        Task DeleteServiceAsync(int serviceId);
    }
}
