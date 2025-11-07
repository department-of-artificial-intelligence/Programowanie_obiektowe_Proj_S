using System;
using System.Collections.Generic;

class Car
{
    private string _brand;
    private string _model;
    private int _doorCount;
    private float _engineVolume;
    private double _avgConsump;
    private string _registrationNumber;

    public string Brand
    {
        get { return _brand; }
        set { _brand = value; }
    }
    public string Model
    {
        get { return _model; }
        set { _model = value; }
    }
    public int DoorCount
    {
        get { return _doorCount; }
        set { _doorCount = value; }
    }
    public float EngineVolume
    {
        get { return _engineVolume; }
        set { _engineVolume = value; }
    }
    public double AvgConsump
    {
        get { return _avgConsump; }
        set { _avgConsump = value; }
    }
    public string RegistrationNumber
    {
        get { return _registrationNumber; }
        set { _registrationNumber = value; }
    }

    private static int carCount = 0;
    public static int CarCount { get { return carCount; } private set { carCount = value; } }

    public Car() : this(string.Empty, string.Empty, 0, 0, 0, string.Empty) { }

    public Car(
        string brand, string model, int doorCount, float engineVolume, double avgConsump, string registrationNumber
        )
    {
        _brand = brand;
        _model = model;
        _doorCount = doorCount;
        _engineVolume = engineVolume;
        _avgConsump = avgConsump;
        _registrationNumber = registrationNumber;
        carCount++;
    }

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
            _brand, _model, _doorCount, _engineVolume, _avgConsump, _registrationNumber
            );
    }
}



//=============================Zadanie 2========================

class Garage
{
    private List<Car> _cars;
    private string _address;
    private int _capacity;

    public string Address { get { return _address; } set { _address = value; } }
    public int Capacity { get { return _capacity; } set { _capacity = value; } }
    public int CarsCount { get { return _cars.Count; } }

    public Garage(): this(string.Empty, 0) { }
    public Garage(string address, int capacity)
    {
        _address = address;
        _capacity = capacity;
        _cars = new List<Car>(_capacity);
    }

    public bool CarIn(Car car)
    {
        if (car == null || _cars.Count>= _capacity) return false;
        foreach (Car c in _cars)
        {
            if (c.Equals(car)) return false;
        }
        _cars.Add(car);
        return true;
    }


    public Car? CarOut()
    {
        if (_cars.Count == 0) return null;

        Car car = _cars[_cars.Count - 1];
        _cars.Remove(car);
        return car;
    }



    public bool CarOut(Car car)
    {
        if(car == null || _cars.Count == 0) return false;
        return _cars.Remove(car);
    }

    public Car? CarOut(string registrationNumber)
    {
        Car? car = null;
        foreach (Car c in _cars)
        {
            if(c.RegistrationNumber == registrationNumber)
            {
                car = c;
                break;
            }
        }
        if (car != null) _cars.Remove(car);
    return car;
    }


    public List<Car> GetCarsByBrand(string brand)
    {
        List<Car> carsByBrand = new List<Car>();
        foreach (Car car in _cars)
        {
            if (car.Brand == brand)
            {
                carsByBrand.Add(car);
            }
        }
        return carsByBrand;
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


//=============================Zadanie 3========================
class Person
{
    private List<Car> _cars;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public int CarsCount { get { return _cars.Count; } }

    public static int MaxCarCount { get; private set; } = 3;

    public Person() : this(string.Empty, string.Empty, string.Empty) { }
    public Person(string firstName, string lastName, string address)
    { 
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Address = address;
        _cars = new List<Car>();
    }

    public Person(string firstName, string lastName, string address, List<Car> cars) : this(firstName, lastName, address) {
        
        if (cars == null) return;

        if (cars.Count > MaxCarCount)
            throw new Exception($"Lista samochodów {cars.Count} przekracza limit: {MaxCarCount}");
        foreach(Car car in cars) _cars.Add(car);
    }

    public bool AddCar(Car car)
    {
        if (car == null || _cars.Count >= MaxCarCount) return false;
        foreach (Car c in _cars)
        {
            if (c.Equals(car)) return false;
        }
        _cars.Add(car);
        return true;
    }

    public Car? RemoveCar(string registrationNumber)
    {
        Car? car = null;
        foreach (Car c in _cars)
        {
            if (c.RegistrationNumber == registrationNumber)
            {
                car = c;
                break;
            }
        }
        if (car != null) _cars.Remove(car);
        return car;
    }

    public bool RemoveCar(Car car)
    {
        if (car == null || _cars.Count == 0) return false;
        return _cars.Remove(car);
    }

    public override string ToString()
    {
        string s = string.Format("Osoba: {0}/{1}/{2}/{3}/posiadane samochody:\n",
            FirstName, LastName, Address, CarsCount);
        foreach (Car car in _cars)
        {
            s += "\n- " + car;
        }
        return s;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--------------");
        Console.WriteLine("Testowanie klasy Car");
        Console.WriteLine("--------------");

        Car car1 = new Car();
        Console.WriteLine(car1);

        Car car2 = new Car("Fiat", "500", 2, 1.2f, 5.2, "FI500");
        Console.WriteLine(car2);

        Car car3 = new Car("Tesla", "Model S", 4, 0f, 15.0, "TS100");
        Console.WriteLine(car3);

        car1.Brand = "BMW";
        car1.Model = "X5";
        car1.DoorCount = 5;
        car1.EngineVolume = 3.0f;
        car1.AvgConsump = 9.5;
        car1.RegistrationNumber = "BMW1234";
        Console.WriteLine(car1);

        double roadLength = 200;
        double petrolCost = 5.2;

        double fuelConsumption = car2.CalculateFuelConsumption(roadLength);
        Console.WriteLine($"Zużycie paliwa dla {roadLength} km: {fuelConsumption} litrów");

        double tripCost = car2.CalculateCostOfTheTrip(roadLength, petrolCost);
        Console.WriteLine($"Koszt paliwa dla {roadLength} km: {tripCost} PLN");

        Console.WriteLine($"Liczba samochodów stworzonych: {Car.CarCount}");
        Console.WriteLine("Opis samochodu car3: " + car3);


        Console.WriteLine("-----------------------------------------------------");
        Console.WriteLine("Testowanie klasy Garage");
        Console.WriteLine("-----------------------------------------------------");

        //Testowanie klasy Garage
        Garage garage = new Garage("Garaż przy ul. Kwiatowej", 2);
        Console.WriteLine("Adres garażu: " + garage.Address);
        Console.WriteLine("Pojemność garażu: " + garage.Capacity);
        Console.WriteLine("Liczba samochodów w garażu: " + garage.CarsCount);

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
        Console.WriteLine("Samochód wyjęty z garażu (bez numeru rejestracyjnego): " + (carOut != null ? carOut.ToString() : "Brak"));

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
        Console.WriteLine($"Średnie zużycie paliwa aut w garażu {avgFuelConsump} l/100km");


        Console.WriteLine("-----------------------------------------------------");
        Console.WriteLine("Testowanie klasy Person");
        Console.WriteLine("-----------------------------------------------------");

        Person person = new Person("Jan", "Kowalski", "Warszawa, ul. Długa 15");
        Console.WriteLine("Imię: " + person.FirstName);
        Console.WriteLine("Nazwisko: " + person.LastName);
        Console.WriteLine("Adres: " + person.Address);
        Console.WriteLine("Liczba samochodów przypisanych do osoby: " + person.CarsCount);

        Console.WriteLine("Przypisywanie samochodów do osoby...");
        person.AddCar(car1);
        person.AddCar(car2);
        Console.WriteLine(person);

        //Sprawdzanie, czy samochód można przypisać ponownie (jest przypisany do osoby)
        bool assignedAgain = person.AddCar(car1); //Próba dodania ponownie car1
        Console.WriteLine($"Próba ponownego przypisania car1 do osoby: {assignedAgain}");

        //Usuwanie samochodu od osoby
        person.RemoveCar(car1);
        Console.WriteLine("Po usunięciu car1 od osoby:");
        Console.WriteLine(person);

        //Testowanie metody RemoveCar() z numerem rejestracyjnym
        carOut = person.RemoveCar("FI500");
        Console.WriteLine("Samochód usunięty od osoby (po nr rejestracyjnym): " + (carOut != null ? carOut.ToString() : "Brak"));


    }
}