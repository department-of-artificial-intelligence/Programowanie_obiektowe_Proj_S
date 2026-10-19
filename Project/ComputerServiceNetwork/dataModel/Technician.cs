using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class Technician: User {
        public string Specialization { get; set; } = string.Empty;
        public bool IsBusy { get; set; } = false;
        public List<Order> AssignedOrders { get; set; } = new List<Order>();

        public string DiagnoseDevice(Device device) {
            return $"Technician {FullName} diagnosed {device.Model}: {device.IssueDescription}";
            }
        }
    }

