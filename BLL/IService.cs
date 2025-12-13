using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

using RatingSystem.DAL;
namespace RatingSystem.BLL
{
    public  interface IService
    {
        Task AddServiceAsync(Service service);
        Task <IEnumerable<Service>> GetServicesAsync();
        Task<Service> GetServiceByIdAsync(int serviceId);
    }
}
