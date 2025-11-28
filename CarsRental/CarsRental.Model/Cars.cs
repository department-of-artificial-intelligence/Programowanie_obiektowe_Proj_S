using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public class Car : IIdentify
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

        private bool _isAvailable;
        public bool IsAvailable { get { return _isAvailable; } set { _isAvailable = value; } }

        private Department _department;
        public Department Department { get { return _department; } set { _department = value; } }

        public Car() : this(0, string.Empty, string.Empty, 0, 0, 0, 0, 0, string.Empty, string.Empty, 0, 0, true, new Department()) { }
        public Car(int id, string brand, string model, int prodYear, double engineVolume, int horsePower, int torque, double timetoHundred, string driveType, string gearboxType, int seats, double basePrice, bool isAvailable, Department department)
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
            _isAvailable = isAvailable;
            _department = department;
        }

        public override string ToString()
        {
            return $"Marka: {Brand} | Model: {Model} | Rok produkcji: {ProdYear} \n" +
                    $"Pojemność: {EngineVolume:F2} L | Moc: {HorsePower} KM | Moment obrotowy: {Torque} NM \n" +
                    $"0-100: {TimetoHundred:F2}s | Napęd: {DriveType} | Skrzynia: {GearboxType} | Miejsca: {Seats} \n" +
                    $"Cena za dzień: {BasePrice} zł \n" + $"Dostępność: {IsAvailable}\n";
        }
    }
}
