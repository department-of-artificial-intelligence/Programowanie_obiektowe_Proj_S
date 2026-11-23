using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO.DTOs
{
    public class RemovedRestaurantDto
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
