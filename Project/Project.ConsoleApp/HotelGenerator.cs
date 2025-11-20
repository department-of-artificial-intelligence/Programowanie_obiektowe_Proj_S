using Project.Model;

namespace Project.ConsoleApp
{
    public class HotelGenerator
    {
        public readonly string[] FirstNames =
        [
            "Luna", "Oliver", "Charlie", "Leo", "Bella", "Lucy", "Milo", "Lily", "Daisy", "Max",
            "Jack", "Jasper", "Rocky", "Simba", "Oscar", "Chloe", "Cleo", "Molly", "Stella", "Rosie"
        ];
        
        public readonly string[] LastNames =
        [
            "Shadow", "Onyx", "Ash", "Midnight", "Raven", "Rusty", "Sunny", "Clementine", "Pumpkin",
            "Smokey", "Ash", "Cinder", "Dusty", "Slate", "Buttercup", "Sassy", "Pixie", "Athena",
            "Queenie", "Chief", "Loki", "Gizmo", "Bandit", "Mochi", "Cookie", "Oreo", "Pepper", "Bean"
        ];
        
        public readonly string HotelAddress = "12 Feline Ave, Catshington DC, 20001";
        public readonly string HotelName = "Cat-ish Hotel";
        
        private Random _random;
        
        public HotelGenerator()
        {
            this._random = new Random();
        }

        public HotelGenerator(int seed)
        {
            this._random = new Random(seed);
        }

        private Person GeneratePerson()
        {
            return new Person()
            {
                FirstName = FirstNames[this._random.Next(FirstNames.Length)],
                LastName = LastNames[this._random.Next(LastNames.Length)],
                DateOfBirth = DateTime.Now.AddYears(-this._random.Next(14, 80))
                    .AddDays(this._random.Next(0, 365)) // in this (definitely better) universe cats live more than 20 years
            };
        }
        
        private List<Person> GeneratePeople(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => GeneratePerson())
                .ToList();
        }

        private List<RoomHistoricResident> GenerateHistoricResidents(int count)
        {
            return this.GeneratePeople(count)
                .Select(x => new RoomHistoricResident()
                {
                    Person = x,
                    ResidentFrom = DateTime.Now.AddDays(-this._random.Next(1001, 2000)),
                    ResidentTo = DateTime.Now.AddDays(-this._random.Next(1, 1000))
                })
                .ToList();
        }
        
        private List<Resident> GenerateResidents(int count)
        {
            return this.GeneratePeople(count)
                .Select(x => new Resident()
                {
                    Person = x,
                    ResidentFrom = DateTime.Now.AddDays(-this._random.Next(1, 1000))
                })
                .ToList();
        }
        
        private HotelRoom GenerateHotelRoom(int number)
        {
            return new HotelRoom()
            {
                Floor = number / 10,
                Number = number,
                Residents = this.GenerateResidents(this._random.Next(1, 3)),
                HistoricResidents = this.GenerateHistoricResidents(this._random.Next(1, 5))
            };
        }
        
        private List<HotelRoom> GenerateHotelRooms(int count)
        {
            return Enumerable.Range(0, count)
                .Select(i => GenerateHotelRoom(i + 1))
                .ToList();
        }
        
        public Hotel GenerateHotel()
        {
            return new Hotel()
            {
                Address = this.HotelAddress,
                Name = this.HotelName,
                Manager = new Manager()
                {
                    Person = GeneratePerson()
                },
                Rooms = this.GenerateHotelRooms(this._random.Next(4, 20))
            };
        }
    }
}