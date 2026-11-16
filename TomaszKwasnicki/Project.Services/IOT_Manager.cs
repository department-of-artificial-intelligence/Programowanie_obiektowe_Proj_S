using Project.Model;
// Musisz zaimportować przestrzeń nazw z projektu Model:
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Services
{

    public class IOT_Manager : IIotManager
    {
        // Lista pokoi pochodząca z projektu Model
        public List<Room> Rooms { get; private set; } = new List<Room>();

        public void AddRoom(string roomName)
        {
            if (!Rooms.Any(r => r.Field == roomName))
            {
                Rooms.Add(new Room { Field = roomName });
            }
        }

        // KLUCZOWY ELEMENT ODPOWIADAJĄCY PROFESOROWI:
        // Zarządzanie i dodawanie nowych urządzeń do odpowiedniego pokoju
        public void RegisterDeviceToRoom(string roomName, Iot_Device device)
        {
            var room = Rooms.FirstOrDefault(r => r.Field == roomName);
            if (room == null)
            {
                // Jeśli pokój nie istnieje, możemy go automatycznie utworzyć
                room = new Room { Field = roomName };
                Rooms.Add(room);
            }

            // Sprawdzamy typ urządzenia (dziedziczenie z Modelu) i dodajemy do odpowiedniej kolekcji
            if (device is Actuating_Device actuatingDevice)
            {
                room.ActuatingDevices.Add(actuatingDevice);
            }
            else if (device is Sensors sensor)
            {
                room.SensorsList.Add(sensor);
            }

            Console.WriteLine($"[Service] Zarejestrowano nowe urządzenie '{device.Name}' w pokoju '{roomName}'.");
        }

        public void UpdateDashboard()
        {
            // Logika odświeżania stanu systemowego / widoków
        }

        public void SaveDataToDB()
        {
            // Logika zapisu stanu do bazy danych
        }
    }
}