using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public interface IIdentify
    {
        int Id { get; set; }
    }

    public interface IManager<T> where T : IIdentify
    {
        void Add(T item);
        List<T> GetAll();
        T? GetById(int id);
        void Remove(int id);
    }
}
