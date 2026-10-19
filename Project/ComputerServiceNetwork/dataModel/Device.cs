using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public class Device {
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public DeviceType Type { get; set; }

        public string GetFullInfo() {
            return $"[{Type}] {Model} (S/N: {SerialNumber}) - Issue: {IssueDescription}";
        }
    }
}
