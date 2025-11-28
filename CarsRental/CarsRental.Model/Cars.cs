using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public interface IReservable
    {
        bool IsAvailable { get; }
        void Reserve(DateTime reservationStart, DateTime reservationEnd);
    }

    public class CarManager : IManager<Car>
    {
        private List<Car> _cars = new List<Car>();
        private int _carCounter = 0;

        public void Add(Car car)
        {
            Console.WriteLine("Dodawanie pojazdu...");

            if (car == null)
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return;
            }

            _carCounter++;
            car.Id = _carCounter;
            _cars.Add(car);

            Console.WriteLine($"Dodano {car.Brand} {car.Model} {car.ProdYear} do wypożyczalni!\n");
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car? GetById(int id)
        {
            var car = _cars.Find(c => c.Id == id);
            if (car == null)
            {
                Console.WriteLine($"Nie znaleziono klienta ID({id})\n");
                return null;
            }
            return car;
        }

        public void Remove(int id)
        {
            Console.WriteLine("Usuwanie pojazdu...");

            Car? carToRemove = GetById(id);
            if (carToRemove != null)
            {
                _cars.Remove(carToRemove);
                Console.WriteLine($"Usunięto {carToRemove.Brand} {carToRemove.Model} {carToRemove.ProdYear} z wypożyczalni\n");
            }
        }

        public List<Car> GetByDepartment(Department department)
        {
            return _cars.Where(c => c.Department == department).ToList();
        }
    }

    public class Car : IIdentify, IReservable
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _brand;
        public string Brand { get { return _brand; } set { _brand = value; } }

        private string _model;
        public string Model { get { return _model; } set { _model = value; } }

        private int _prodYear;
        public int ProdYear { get { return _prodYear; } set { _prodYear = value; } }

        private double _engineVolume;
        public double EngineVolume { get { return _engineVolume; } set { _engineVolume = value; } }

        private int _horsePower;
        public int HorsePower { get { return _horsePower; } set { _horsePower = value; } }

        private int _torque;
        public int Torque { get { return _torque; } set { _torque = value; } }

        private double _timetoHundred;
        public double TimetoHundred { get { return _timetoHundred; } set { _timetoHundred = value; } }

        private string _driveType;
        public string DriveType { get { return _driveType; } set { _driveType = value; } }

        private string _gearboxType;
        public string GearboxType { get { return _gearboxType; } set { _gearboxType = value; } }

        private int _seats;
        public int Seats { get { return _seats; } set { _seats = value; } }

        private double _basePrice;
        public double BasePrice { get { return _basePrice; } set { _basePrice = value; } }

        private string _status;
        public string Status { get { return _status; } set { _status = value; } }

        private Department _department;
        public Department Department { get { return _department; } set { _department = value; } }

        public Car() : this(0, string.Empty, string.Empty, 0, 0, 0, 0, 0, string.Empty, string.Empty, 0, 0, string.Empty, new Department()) { }
        public Car(int id, string brand, string model, int prodYear, double engineVolume, int horsePower, int torque, double timetoHundred, string driveType, string gearboxType, int seats, double basePrice, string status, Department department)
        {
            _id = id;
            _brand = brand;
            _model = model;
            _prodYear = prodYear;
            _engineVolume = engineVolume;
            _horsePower = horsePower;
            _torque = torque;
            _timetoHundred = timetoHundred;
            _driveType = driveType;
            _gearboxType = gearboxType;
            _seats = seats;
            _basePrice = basePrice;
            _status = status;
            _department = department;
        }

        public bool IsAvailable => Status == "Dostępny";

        public void Reserve(DateTime reservationStart, DateTime reservationEnd)
        {
            if (!IsAvailable)
            {
                Console.WriteLine($"{Brand} {Model} {ProdYear} jest już zarezerwowany!\n");
                return;
            }

            Status = $"Wypożyczony do {reservationEnd:dd.MM.yyyy}";
        }

        public override string ToString()
        {
            return $"Marka: {Brand} | Model: {Model} | Rok produkcji: {ProdYear} \n" +
                    $"Pojemność: {EngineVolume:F2} L | Moc: {HorsePower} KM | Moment obrotowy: {Torque} NM \n" +
                    $"0-100: {TimetoHundred:F2}s | Napęd: {DriveType} | Skrzynia: {GearboxType} | Miejsca: {Seats} \n" +
                    $"Cena za dzień: {BasePrice} zł \n" + $"Dostępność: {Status}\n";
        }
    }
}
