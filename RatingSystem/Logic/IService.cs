using System;
using System.Collections.Generic;
using System.Text;

namespace RatingSystem.Logic
{
    public  interface IService
    {
        Task AddServiceAsync(IService service);
        Task <IEnumerable<IService>> GetServicesAsync();
    }
}
