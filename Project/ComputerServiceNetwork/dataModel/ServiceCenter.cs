using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class ServiceCenter {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<Technician> Staff { get; set; } = new List<Technician>();
        public List<SparePart> Inventory { get; set; } = new List<SparePart>();

        public Technician? GetAvailableTechnician() {
            return Staff.Find(t => !t.IsBusy);
        }
    }
}
