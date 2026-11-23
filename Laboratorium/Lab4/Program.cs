using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab2
{
    class Subject
    {

        public string Name { get; set; }
        private string  _specialization;
        public string Specialization
        {
            get { return _specialization; }
            set
            {
                if (value != null && value != "") { _specialization = value; }
                else { throw new ArgumentException("Nie może być puste."); }
            }
        }
        private int _semester;
        public int Semester
        {
            get { return _semester; }
            set
            {
                if (value > 0) { _semester = value; }
                else { throw new ArgumentException("Większe od 0."); }
            }
        }
        private int _hoursCount;
        public int HoursCount
        {
            get { return _hoursCount; }
            set
            {
                if (value > 0) { _hoursCount = value; }
                else { throw new ArgumentException("Większe od 0."); }
            }
        }


        public Subject(string Name, string Specialization, int Semester, int HoursCount) {
            this.Name = Name;
            this.Specialization= Specialization;
            this.Semester = Semester;
            this.HoursCount = HoursCount;
        }

        public override string ToString()
        {
            return $"{Name}/{Specialization}/{Semester}/{HoursCount}";
        }

    }

    class FinalGrade
    {

        private Subject _subject;
        public Subject Subject
        {
            get { return _subject; }
            set
            {
                if (value != null) { _subject = value; }
                else { throw new ArgumentException("Nie null."); }
            }
        }
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (value > 2.0) { _value = value; }
                else { throw new ArgumentException(">2.0."); }
            }
        }
        public DateTime Data { get; set; }

        public FinalGrade(Subject Subject, double Value, DateTime Data) {
            this.Subject = Subject;
            this.Value = Value;
            this.Data = Data;
        }

        public override string ToString()
        {
            return $"Ocena kończowa: {Subject}/{Data.ToShortDateString()}/{Value}";
        }
    }

    interface IClassWithIList { }









    //statyczna klasa rozszerzen wyswietla wszystkie wlasciwosci z klasy ktora rozszerza classwithlist, struktura swoich wlasociwosci

    static class Rozszerzam
    {

        public static IList<T> GetList2<T>(this IClassWithIList obj)
        {
            if (obj == null)
                return null;

            var property = obj.GetType().GetProperties();
                //.FirstOrDefault(p => p.PropertyType == typeof(IList<T>));

            if (property == null)
                return null;

            foreach(var t in property)
            {
                Console.WriteLine(t);
            }

            return property as IList<T>;
        }
    }







    class Person : IClassWithIList
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Person(string FirstName, string LastName, DateTime DateOfBirth) {
            this.FirstName = FirstName;
        this.LastName = LastName;
       this.DateOfBirth = DateOfBirth;
        }

        public override string ToString()
        {
            return $"Osoba:{FirstName} {LastName}/{DateOfBirth.ToShortDateString()}";
        }
    }

    class Lecturer : Person
    {
        public string AcademicTitle { get; set; }
        public string Position { get; set; }

        public Lecturer(string firstName, string lastName, DateTime dateOfBirth,
                    string academicTitle, string position)
        : base(firstName, lastName, dateOfBirth)
        {
            AcademicTitle = academicTitle;
            Position = position;
        }

        public override string ToString()
        {
            return $"{base.ToString()} + {AcademicTitle}/{Position}";
        }
    }

    class Student : Person
    {
        private static int _id;
        public static int Id { get; }
        public IList<FinalGrade> Grades { get; set; } = [];
        public string Specialization { get; set; }
        private int _group;
        public int Group
        {
            get { return _group; }
            set
            {
                if (value > 0) { _group = value; }
                else { throw new ArgumentException("Wieksze od 0."); }
            }
        }
        private int _semester;
        public int Semester
        {
            get { return _semester; }
            set
            {
                if (value > 0) { _semester = value; }
                else { throw new ArgumentException("Wieksze od 0."); }
            }
        }
        public int IndexId { get; set; }
        public double AverageGrades { get; }

        public Student(string firstName, string lastName, DateTime dateOfBirth,
            string Specialization, int Group, int Semester)
        : base(firstName, lastName, dateOfBirth)
        {
            this.Specialization = Specialization;
            this.Group = Group;
            this.Semester = Semester;
        }

        public override string ToString()
        {
            return base.ToString() + string.Join("\n:", Grades);
        }
    }

    class OrganizationUnit : IClassWithIList
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { _name = value; }
                else { throw new ArgumentException("Nie null."); }
            }
        }
        private string _address;
        public string Address
        {
            get { return Address; }
            set
            {
                if (value != null) { _address = value.Trim(); ; }
                else { throw new ArgumentException("Nie null"); }
            }
        }
        public IList<Lecturer> Lecturers { get; set; } = [];

        public OrganizationUnit(string Name, string Address, IList<Lecturer> Lecturers) {
            this.Name = Name;
            this.Address = Address;
            this.Lecturers = Lecturers;
        }

        public override string ToString()
        {
            return $"{Name}/{Address}/" + string.Join("\n:", Lecturers);
        }

    }
    class Department : IClassWithIList
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null) { _name = value; }
                else { throw new ArgumentException("Nie null"); }
            }
        }
        private Person _dean;
        public Person Dean
        {
            get { return _dean; }
            set
            {
                if (value != null && value is Lecturer) { _dean = value; }
                else { throw new ArgumentNullException(nameof(value)); }
            }
        }
        public IList<OrganizationUnit> OrganizationUnits { get; set; }
        public IList<Subject> Subjects { get; set; }
        public IList<Student> Students { get; set; }

        public Department(string Name, Person Dean, IList<Subject> Subjects, IList<Student> Students)
        {
            this.Name = Name;
            this.Dean = Dean;
            this.Subjects = Subjects;
            this.Students = Students;

        }
        public override string ToString()
        {
            return $"{Name}/{Dean}/{OrganizationUnits}/{Students}";
        }
    }

    static class ClassWithIListGetListExtension
    {
        public static IList<T> GetList<T>(this IClassWithIList obj)
        {
            if (obj == null)
                return null;

            var property = obj.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.PropertyType == typeof(IList<T>));

            if (property == null)
                return null;

            return property.GetValue(obj) as IList<T>;
        }
    }

    static class ClassWithIListCrudOperationsExtension
    {
        public static void Add<T>(this IClassWithIList obj, T itemToAdd)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null) list.Add(itemToAdd);
        }
        public static void AddRange<T>(this IClassWithIList obj, IList<T> itemsToAdd)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null && itemsToAdd != null)
                foreach (T item in itemsToAdd)
                    list.Add(item);
        }
        public static void Clear<T>(this IClassWithIList obj)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null)
                list.Clear();
        }

        public static bool Contains<T>(this IClassWithIList obj, T itemToCheck)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null)
                return list.Contains(itemToCheck);
            else return false;
        }

        public static int IndexOf<T>(this IClassWithIList obj, T itemToFind)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null)
                return list.IndexOf(itemToFind);
            else
                return -1;
        }

        public static bool Remove<T>(this IClassWithIList obj, T itemToRemove)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null)
                return list.Remove(itemToRemove);
            else return false;
        }

        public static void RemoveAt<T>(this IClassWithIList obj, int index)
        {
            IList<T> list = obj.GetList<T>();
            if (index >= 0 && index < list.Count)
                list.RemoveAt(index);
        }

        public static void Update<T>(this IClassWithIList obj, T oldItem, T newItem)
        {
            IList<T> list = obj.GetList<T>();
            if (list != null)
            {
                int index = list.IndexOf(oldItem);
                list[index] = newItem;
            }
        }

        public static void ForEach<T>(this IClassWithIList obj, Action<T> action)
        {
            IList<T> list = obj.GetList<T>();
            foreach (var e in list) action(e);
        }

        public static void RemoveAll<T>(this IClassWithIList obj, Func<T, bool> predicate)
        {
            IList<T> list = obj.GetList<T>();

            if (list != null && predicate != null)
            {
                var itemsToRemove = list.Where(predicate).ToList();
                foreach (T item in itemsToRemove) list.Remove(item);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //-----------------------------------------------------------------------
            // Przygotowanie danych
            //-----------------------------------------------------------------------
            #region Przygotowanie danych
            Subject soop = new("Programowanie obiektowe", "Informatyka", 2, 60);
            Subject sdb = new("Bazy danych", "Informatyka", 3, 45);
            Subject sweb = new("Technologie internetowe", "Informatyka", 4, 45);
            Subject sma = new("Matematyka", "Informatyka", 1, 30);
            Console.WriteLine($"[] Przedmioty po zainicjowaniu:\n{soop}\n{sdb}\n{sweb}\n");
            Lecturer l1 = new("Jan", "Nowak", new(2040, 6, 1), "dr inż.", "Adiunkt");
            Lecturer l2 = new("Maja", "Kot", new(2030, 8, 15), "dr", "Asystent");
            Lecturer l3 = new("Jan", "Walas", new(2045, 12, 6), "prof.", "Profesor");
            Console.WriteLine($"[] Wykładowcy po zainicjowaniu:\n{l1}\n{l2}\n{l3}\n");
            Student s1 = new("Anna", "Misiak", new(2055, 4, 1), "Informatyka", 1, 2);
            Student s2 = new("Ewa", "Kowalska", new(2055, 3, 8), "Informatyka", 1, 3);
            Student s3 = new("Olga", "Siejska", new(2055, 1, 6), "Informatyka", 2, 3);
            Console.WriteLine($"[] Studenci po zainicjowaniu:\n{s1}\n{s2}\n{s3}\n");
            IList<FinalGrade> grades1 = [
            /*new(soop, 5.0, new(2075,6,1)),*/
            new(sdb, 4.0, new(2075,5,1)),
            new(sweb, 3.5, new(2075,4,1))
            ];
            IList<FinalGrade> grades2 = [
            new(soop, 3.0, new(2075,6,1)),
                new(sdb, 2.0, new(2075,5,1)),
                new(sweb, 4.0, new(2075,4,15))
            ];
            IList<FinalGrade> grades3 = [
            new(soop, 5.0, new(2075,6,1)),
            new(sdb, 5.0, new(2075,3,8)),
            new(sweb, 5.0, new(2075,3,15))
            ];
            Console.WriteLine($"[] Listy ocen po zainicjowaniu:\n{ grades1}\n{grades2}\n{ grades3}\n"); //brak rozszerzenia, można dodac
            OrganizationUnit ou1 = new("Katedra TI", "ul. Polna 1", [l1, l2]);
            OrganizationUnit ou2 = new("Katedra BD", "ul. Polna 2", [l3]);
            Lecturer dean = new("Andrzej", "Lewandowski", new DateTime(2035, 2, 14), "dr hab. inż.","Dziekan");
            Department d1 = new("WIiSI", dean, [soop, sdb, sweb, sma], [s1, s2, s3]);
            Console.WriteLine($"[] Wydział po zainicjowaniu:\n{d1}\n");
            #endregion
            //-----------------------------------------------------------------------
            // Testowanie metod rozszerzeń (TMR): Add, AddRange
            //-----------------------------------------------------------------------
            Console.WriteLine($"[] Studenci przed dodaniem ocen:\n{s1}\n{s2}\n{s3}\n");
            s1.Add<FinalGrade>(new(soop, 5.0, new(2075, 6, 1)));
            s1.AddRange<FinalGrade>(grades1);
            s2.AddRange<FinalGrade>(grades2);
            s3.AddRange<FinalGrade>(grades3);
            Console.WriteLine($"[] Studenci po dodaniu ocen:\n{s1}\n{s2}\n{s3}\n");
            d1.AddRange<OrganizationUnit>([ou1, ou2]);
            Console.WriteLine($"[] Wydział po dodaniu jednostek:\n{d1}\n");
            //-----------------------------------------------------------------------
            // TMR: Contains, IndexOf, Remove
            //-----------------------------------------------------------------------
            FinalGrade nowaOcena = new(sdb, 4.5, new(2075, 7, 1));
            s1.Add<FinalGrade>(nowaOcena);
            bool contains1 = s1.Contains<FinalGrade>(nowaOcena); //true
            bool contains2 = s1.Contains<FinalGrade>(new(sdb, 4.5, new(2075, 7, 4))); //false
            Console.WriteLine($"[] contains1={contains1}, contains2={contains2}\n");
            int index = s1.IndexOf<FinalGrade>(nowaOcena); //3
            Console.WriteLine($"[] index={index}\n");
            s1.Remove<FinalGrade>(nowaOcena);
            Console.WriteLine($"[] s1 po usunięciu nowej oceny:\n{s1}\n");
            //-----------------------------------------------------------------------
            // Dodatkowy test: RemoveAt
            //-----------------------------------------------------------------------
            Console.WriteLine("[] Test RemoveAt:");
            FinalGrade ocenaMatematyka = new(sma, 5.0, new(2075, 9, 1));
            s1.Add<FinalGrade>(ocenaMatematyka);
            int idx = s1.IndexOf<FinalGrade>(ocenaMatematyka);
            if (idx != -1) s1.RemoveAt<FinalGrade>(idx);
            Console.WriteLine($"s1 po RemoveAt({idx}):\n{s1}\n");
            //-----------------------------------------------------------------------
            // TMR: Update
            //-----------------------------------------------------------------------
            Console.WriteLine($"[] s2 przed poprawą oceny z BD:\n{s2}\n");
            var stara = s2.Grades.FirstOrDefault(g => g.Subject.Name == "Bazy danych"&&g.Value==2);
            s2.Update<FinalGrade>(stara, new(sdb, 4.5, new DateTime(2075, 9, 30)));
            Console.WriteLine($"[] s2 po poprawie oceny z BD:\n{s2}\n");
            //-----------------------------------------------------------------------
            // TMR: grupowanie (dla chętnych na wyższą liczbę punktów)_
            //-----------------------------------------------------------------------
            //Console.WriteLine("[] Grupowanie ocen s1 po nazwie przedmiotu:");
            //var grupyA = s1.GroupBy<FinalGrade, string>(g => g.Subject.Name);
            //foreach (var g in grupyA) Console.WriteLine(g);
            //Console.WriteLine();
            //Console.WriteLine("[] Grupowanie ocen s3 po wartości i nazwie przedmiotu:");
            //var grupyB = s3.GroupBy<FinalGrade, double, string>(
            //g => g.Value,
            //g => g.Subject.Name
            //);
            //foreach (var g in grupyB) Console.WriteLine(g);
            //Console.WriteLine();
            //-----------------------------------------------------------------------
            // TMR: ForEach, RemoveAll
            //-----------------------------------------------------------------------
            Console.WriteLine("[] Oceny s2 (ForEach):");
            s2.ForEach<FinalGrade>(g => Console.WriteLine($"{s2.FirstName} ma ocenę {g.Value} z { g.Subject.Name}."));
            Console.WriteLine();
            s2.RemoveAll<FinalGrade>(g => g.Value < 3.0);
            Console.WriteLine($"[] s2 po usunięciu ocen niedostatecznych: {s2}\n");
            d1.RemoveAll<Student>(s => s.FirstName == "Anna");
            Console.WriteLine($"[] Wydział po usunięciu studentów o imieniu 'Anna': {d1}\n");
            //-----------------------------------------------------------------------
            // TMR: Clear, ForEach
            //-----------------------------------------------------------------------
            Console.WriteLine("[] Przedmioty przed usunięciem:");
            d1.ForEach<Subject>(p => Console.WriteLine(p));
            d1.Clear<Subject>();
            Console.WriteLine();
            Console.WriteLine("[] Wykaz przedmiotów po usunięciu:");
            d1.ForEach<Subject>(p => Console.WriteLine(p));
            Console.WriteLine();




            //--------------------------------------------------------------------------

        }
    }
}
