using Project.Model;

namespace Project.Services
{
    public interface IIotManager
    {
        void AddRoom(string roomName); // Powinno być tylko raz!
        void RegisterDeviceToRoom(string roomName, Iot_Device device);
        void UpdateDashboard();
        void SaveDataToDB();
    }
}