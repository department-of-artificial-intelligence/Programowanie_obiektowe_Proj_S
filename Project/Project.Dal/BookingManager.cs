using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    // Klasa zarządza logiką dodawania slotów, wyświetlania dostępności i rezerwacji sesji.
    public class BookingManager
    {
        private readonly DataManagement _dataManager;

        // Dostępność jest teraz pobierana i zapisywana bezpośrednio w bazie danych (DataManagement).

        public BookingManager(DataManagement dataManager)
        {
            _dataManager = dataManager;
        }

        // =========================================================
        // 1. DYNAMICZNE ZARZĄDZANIE SLOTAMI (CRUD)
        // =========================================================

        // Zapisuje nowy wolny termin w bazie danych.
        public void AddTrainerSlot(int trainerId, DateTime slot)
        {
            // Sprawdzenie, czy slot już istnieje (walidacja na poziomie bazy)
            bool exists = _dataManager.TrainerSlots
                .Any(ts => ts.TrainerId == trainerId && ts.SlotTime == slot);

            if (exists)
            {
                Console.WriteLine("Ostrzeżenie: Ten termin już istnieje w bazie i nie został dodany ponownie.");
                return;
            }

            // Utworzenie nowej encji TrainerSlot
            var newSlot = new TrainerSlot(trainerId, slot);

            // ZAPIS DO BAZY
            _dataManager.AddTrainerSlot(newSlot);

            Console.WriteLine($"✅ Dodano nowy wolny termin: {slot.ToString("yyyy-MM-dd HH:mm")}. Zapisano w bazie.");
        }

        // =========================================================
        // 2. WIDOK DOSTĘPNOŚCI
        // =========================================================

        // Wyświetla wolne sloty dla danego trenera (pobierane z bazy).
        public void DisplayTrainerAvailability(Trainer trainer)
        {
            Console.WriteLine($"\n--- DOSTĘPNOŚĆ TRENERA {trainer.LastName} ({trainer.Specialization}) ---");

            // Pobranie przyszłych slotów trenera z bazy
            var futureAvailableSlots = _dataManager.TrainerSlots
                .Where(ts => ts.TrainerId == trainer.Id &&
                             ts.SlotTime > DateTime.Now) // Tylko przyszłe terminy
                .OrderBy(ts => ts.SlotTime)
                .ToList();

            if (futureAvailableSlots.Any())
            {
                foreach (var slot in futureAvailableSlots)
                {
                    // Wyświetlenie ID slotu ułatwia ewentualne zarządzanie/debugowanie
                    Console.WriteLine($"[ID Slotu: {slot.Id}] -> {slot.SlotTime.ToString("yyyy-MM-dd HH:mm")}");
                }
            }
            else
            {
                Console.WriteLine("Brak wolnych terminów w przyszłości.");
            }
        }

        // =========================================================
        // 3. LOGIKA REZERWACJI
        // =========================================================

        // Rezerwuje sesję, usuwając slot z dostępności (TrainerSlot) i dodając Reservation.
        public bool BookSession(int clientId, int trainerId, DateTime slot)
        {
            // Pobranie obiektów Klienta i Trenera
            var client = _dataManager.GetClientById(clientId);
            var trainer = _dataManager.GetTrainerById(trainerId);

            if (client == null || trainer == null)
            {
                Console.WriteLine("Błąd: Nie znaleziono klienta lub trenera.");
                return false;
            }

            // 1. Sprawdzenie, czy slot jest wolny w bazie
            var availableSlot = _dataManager.TrainerSlots
                .FirstOrDefault(ts => ts.TrainerId == trainerId &&
                                      ts.SlotTime == slot);

            if (availableSlot != null)
            {
                // 2. Usunięcie slotu z dostępności (rezerwacja - slot jest 'zużyty')
                _dataManager.RemoveTrainerSlot(availableSlot);

                // 3. Utworzenie nowej Reservation i zapis do bazy (trwały zapis rezerwacji)
                var newReservation = new Reservation(trainer, client, slot);
                _dataManager.AddReservation(newReservation);

                Console.WriteLine($"Zarezerwowano termin dla {client.FirstName} {client.LastName} u Trenera {trainer.LastName} na: {slot.ToString("yyyy-MM-dd HH:mm")}.");
                return true;
            }
            else
            {
                Console.WriteLine("Błąd rezerwacji: Wybrany termin jest już zajęty lub nie istnieje w dostępności.");
                return false;
            }
        }
    }
}