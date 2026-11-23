using System.Net;

namespace Lab1
{
    class Car
    {
        //pola
        private string? _brand;
        private string? _model;
        private int _doorCount;
        private float _engineVolume;
        private double _avgConsump;
        private string? _registrationNumber;

        //własciwości
        public string? Brand { get { return _brand; } set { _brand = value; } }
        public string? Model { get { return _model; } set { _model = value; } }
        public int DoorCount { get { return _doorCount; } set { _doorCount = value; } }
        public float EngineVolume
        {
            get { return _engineVolume; }
            set { _engineVolume = value; }
        }
        public double AvgConsump { get { return _avgConsump; } set { _avgConsump = value; } }
        public string? RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        //pole statyczne
        private static int carCount = 0;
        //właściwość statyczna
        public static int CarCount
        {
            get { return carCount; }
            private set { carCount = value; }
        }

        //konstruktor parametryczny
        public Car(string brand, string model, int doorCount, float engineVolume, double avgConsump, string registrationNumber)
        {
            _brand = brand;
            _model = model;
            _doorCount = doorCount;
            _engineVolume = engineVolume;
            _avgConsump = avgConsump;
            _registrationNumber = registrationNumber;
            carCount++;
        }
        //konstruktor domyślny (łańcuchowe wywołanie)
        public Car() : this(string.Empty, string.Empty, 0, 0, 0, string.Empty) { }

        public double CalculateFuelConsumption(double roadLength)
        {
            return _avgConsump * roadLength / 100.0;
        }

        public double CalculateCostOfTheTrip(double roadLength, double petrolCost)
        {
            return CalculateFuelConsumption(roadLength) * petrolCost;
        }

        public override string ToString()
        {
            return string.Format(
                "Samochód: {0}/{1}/{2}/{3}/{4}/{5}",
                _brand, _model, _doorCount, _engineVolume,
                _avgConsump, _registrationNumber);
        }

    }

    class Garage {
        private List<Car>? _cars;
        private string? _address;
        private int _capacity;

        public string? Address { get { return _address; } set { _address = value; } }
        public int Capacity { get { return _capacity; } set { _capacity = value; } }

        //public int CarCount { get { return _cars.Count; } }
        public int CarCount { get { return _cars?.Count ?? 0; } }

        public Garage(string address, int capacity) {
            _address = address;
            _capacity = capacity;
            _cars = new List<Car>(_capacity);
        }
        public Garage() : this(string.Empty, 0) { }

        //metoda dodawania samochodu
        public bool CarIn(Car car) {
            if (car == null || _cars?.Count >= _capacity) return false;
            foreach(Car c in _cars){
                if (c.Equals(car)) return false;
            }
            _cars.Add(car);
            return true;
        }

        public Car? CarOut()
        {
            if (_cars?.Count == 0) return null;

            Car usuwany = _cars[_cars.Count - 1];
            _cars.RemoveAt(_cars.Count - 1);
            return usuwany;
        }

        public bool CarOut(Car car)
        {
            return _cars.Remove(car);
        }

        public Car? CarOut(string registrationNumber)
        {
            foreach (Car c in _cars)
            {
                if (c.RegistrationNumber == registrationNumber)
                {
                    _cars.Remove(c);
                    return c;
                }
            }
            return null;
        }

        public double CalculateAverageFuelConsumption()
        {
            double sum = 0;
            foreach (Car car in _cars)
            {
                sum += car.AvgConsump;
            }
            return _cars.Count == 0 ? 0 : sum / _cars.Count;
        }

        public List<Car>? GetCarsByBrand(string? typ) { 
            return _cars?.Where(c => c.Brand == typ).ToList();
        }

        public override string ToString()
        {
            string s = string.Format("Garaż {0} ({1}/{2} samochodów):", _address, _cars.Count, _capacity);
            foreach (Car car in _cars)
            {
                s += "\n- " + car;
            }
            return s;
        }

    }
    class Person {
        private List<Car> _cars;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int CarsCount { get { return _cars.Count; } }

        public static int MaxCarCount { get; private set; } = 3;


        public Person(string firstName, string lastName, string address)
        {
            _cars = new List<Car>();
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public Person(List<Car> cars, string firstName, string lastName, string address)
        {
            if (cars.Count <= MaxCarCount)
            {
                _cars = new List<Car>(cars);
            }
            else {
                Console.WriteLine($"(Nie udało się przypisać wszystkich aut do osoby, ponieważ jest ich za dużo. Maksymalnie: {MaxCarCount}.)");
                _cars = cars.Take(MaxCarCount).ToList(); //nauczyc sie metod list
            }

            FirstName = firstName;
            LastName = lastName;
            Address = address;

        }

        public Person() : this([], string.Empty, string.Empty, string.Empty){}

        public bool AddCar(Car car) {

            if (car == null || _cars.Count >= MaxCarCount) return false;
            foreach (Car c in _cars)
            {
                if (c.Equals(car)) return false;
            }
            _cars.Add(car);
            return true;
        }

        public Car? RemoveCar(string registrationNumber) {
            foreach (Car c in _cars)
            {
                if (c.RegistrationNumber == registrationNumber)
                {
                    _cars.Remove(c);
                    return c;
                }
            }
            return null;
        }

        public bool RemoveCar(Car car) {
            return _cars.Remove(car);
        }

        public override string ToString() {
            string s = string.Format("Osoba: {0} {1}/{2}/{3}/posiadane samochody:\n",
            FirstName, LastName, Address, CarsCount);

            foreach (Car car in _cars)
            {
                s += "\n- " + car;
            }

            return s;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Testowanie klasy Car");
            Console.WriteLine("-----------------------------------------------------------------------");
            //Samochód bez parametrów (domyślny konstruktor)
            Car car1 = new Car();
            Console.WriteLine(car1); //Sprawdzamy domyślne wartości pól samochodu
                                     //Samochód z parametrami (konstruktor z parametrami)
            Car car2 = new Car("Fiat", "500", 2, 1.2f, 5.2, "FI500");
            Console.WriteLine(car2); //Sprawdzamy wartości po przypisaniu danych
            Car car3 = new Car("Tesla", "Model S", 4, 0f, 15.0, "TS100");
            Console.WriteLine(car3); //Sprawdzamy inny pojazd
                                     //Testowanie właściwości
            car1.Brand = "BMW";
            car1.Model = "X5";
            car1.DoorCount = 5;
            car1.EngineVolume = 3.0f;
            car1.AvgConsump = 9.5;
            car1.RegistrationNumber = "BMW1234";
            Console.WriteLine(car1); //Właściwości po zmianach
                                     //Testowanie metod
            double roadLength = 200; //Długość trasy w km
            double petrolCost = 5.2; //Koszt paliwa za litr
            double fuelConsumption = car2.CalculateFuelConsumption(roadLength);
            Console.WriteLine($"Zużycie paliwa dla {roadLength} km: {fuelConsumption} litrów");
            double tripCost = car2.CalculateCostOfTheTrip(roadLength, petrolCost);
            Console.WriteLine($"Koszt paliwa dla {roadLength} km: {tripCost} PLN");
            //Testowanie statycznej zmiennej CarCount
            Console.WriteLine($"Liczba samochodów stworzonych: {Car.CarCount}"); //Powinna wynosić 3
            //Testowanie metody ToString()
            Console.WriteLine("Opis samochodu car3: " + car3);


            //--------------------------------------------------------------------
            //--------------------------------------------------------------------
            //--------------------------------------------------------------------
            //--------------------------------------------------------------------
            //--------------------------------------------------------------------


            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Testowanie klasy Garage");
            Console.WriteLine("-----------------------------------------------------------------------");
            //Testowanie klasy Garage
            Garage garage = new Garage("Garaż przy ul. Kwiatowej", 2);
            Console.WriteLine("Adres garażu: " + garage.Address);
            Console.WriteLine("Pojemność garażu: " + garage.Capacity);
            Console.WriteLine("Liczba samochodów w garażu: " + garage.CarCount);
            //Dodawanie samochodów do garażu
            Console.WriteLine("Dodawanie samochodów do garażu...");
            garage.CarIn(car1);
            garage.CarIn(car2);
            Console.WriteLine(garage);
            //Sprawdzanie, czy samochód można dodać ponownie (jest już w garażu)
            bool addedAgain = garage.CarIn(car1); //Próba dodania ponownie car1
            Console.WriteLine($"Próba ponownego dodania car1 do garażu: {addedAgain}");
            //Usuwanie samochodu z garażu
            garage.CarOut(car1);
            Console.WriteLine("Po usunięciu car1 z garażu:");
            Console.WriteLine(garage);
            //Testowanie metody CarOut() bez argumentów
            Car? carOut = garage.CarOut();
            Console.WriteLine("Samochód wyjęty z garażu (bez numeru rejestracyjnego): " + (carOut != null ?
            carOut.ToString() : "Brak"));
            //Testowanie metody CarOut() z numerem rejestracyjnym
            carOut = garage.CarOut("FI500");
            Console.WriteLine("Samochód wyprowadzony z garażu: " + (carOut != null ? carOut.ToString() : "Brak"));
            //Testowanie metody GetCarsByBrand()
            Console.WriteLine("Samochody marki Fiat w garażu:");
            foreach (Car c in garage.GetCarsByBrand("Fiat"))
            {
                Console.WriteLine(c);
            }
            //Testowanie metody CalculateAverageFuelConsumption()
            double avgFuelConsump = garage.CalculateAverageFuelConsumption();
            Console.WriteLine($"Średnie zużycie paliwa aut w garażu: {avgFuelConsump} l/100km");




            //--------------------------------------------------------------------
            //--------------------------------------------------------------------
            //--------------------------------------------------------------------
            //--------------------------------------------------------------------
            //--------------------------------------------------------------------

            List<Car> samochodyFirmy = new List<Car> {
                new Car("Fiat", "500", 2, 1.2f, 5.2, "FI500"),
                new Car("Tesla", "Model S", 4, 0f, 15.0, "TS100"),
                new Car("BMW", "X5", 5, 3.0f, 9.5, "BMW1234"),
                new Car("Toyota", "Corolla", 4, 1.8f, 6.5, "TOY5678")
            };

            Person Adam = new Person("Adam", "Adamiak", "Ażurowa 2");
            Console.WriteLine($"Adam: {Adam}\n");

            Person Bartek = new Person(samochodyFirmy, "Bartek", "Brylant", "Bananowa 5");
            Console.WriteLine($"Bartek: {Bartek}\n");

            samochodyFirmy.RemoveAt(samochodyFirmy.Count - 1);
            Person Bogdan = new Person(samochodyFirmy, "Bogdan", "Brylant", "Bordowa 3");
            Console.WriteLine($"Bogdan: {Bogdan}\n");

            Person Celina = new Person();
            Console.WriteLine($"Celina: {Celina}\n");


            Bogdan.AddCar(car1);
            Console.WriteLine($"Bogdan (dodaje car1): {Bogdan}\n");

            Bogdan.RemoveCar("FI500");
            Console.WriteLine($"Bogdan (zabieram FI500): {Bogdan}\n");

            Bogdan.AddCar(car1);
            Console.WriteLine($"Bogdan (dodaje car1): {Bogdan}\n");

            Bogdan.RemoveCar(car1);
            Console.WriteLine($"Bogdan (zabieram car1): {Bogdan}\n");


        }
    }
}