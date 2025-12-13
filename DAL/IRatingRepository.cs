using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
using RatingSystem;
namespace RatingSystem.DAL { 

    public interface IRatingRepository
    {   
        Task AddAsync(Rating rating);
        Task<Rating> GetByIdAsync(int id);
        Task<IEnumerable<Rating>> GetByServiceIdAsync(int serviceId);

       
    }
}
