using System;
using Project.Services; // Dostęp do menedżera zarządzającego (IIotManager, IOT_Manager)
using Project.Model;

namespace Project.ConsoleAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- URUCHAMIANIE SYSTEMU SMART HOME ---");

            // 1. Inicjalizacja warstwy Services (Menedżer zarządzania)
            IIotManager iotManager = new IOT_Manager();

            // 2. Dodawanie nowego pokoju poprzez serwis
            iotManager.AddRoom("Salon");

            // 3. Tworzenie obiektów z warstwy Model
            //var radiator = new Radiator { Name = "Grzejnik Salon" };
            //var tempSensor = new TemperatureSensor { Name = "Czujnik Temp Salon", MinTemp = 18f, MaxTemp = 24f };

            // 4. Użycie serwisu do zarządzania nowymi urządzeniami (rejestracja w pokoju)
            //iotManager.RegisterDeviceToRoom("Salon", radiator);
            //iotManager.RegisterDeviceToRoom("Salon", tempSensor);

            Console.WriteLine("\nKonfiguracja wstępna zakończona pomyślnie.");
            Console.ReadKey();
        }
    }
}