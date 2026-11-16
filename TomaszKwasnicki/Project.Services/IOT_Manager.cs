using System.Collections.Generic;

namespace MyHome.Model
{
    public class IOT_Manager : IIotManager
    {
        public string RoomName { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();

        public void UpdateDashboard() { /* Implementacja */ }
        public void SaveDataToDB() { /* Implementacja */ }
    }
}