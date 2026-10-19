using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerServiceNetwork.dataModel {
    public enum OrderStatus {
        New,
        InDiagnostics,
        WaitingForParts,
        InProgress,
        Ready,
        Closed
    }

    public enum DeviceType { 
    Laptop,
    DesktopPC,
    GPU,
    Motherboard,
    Other
    }
}
