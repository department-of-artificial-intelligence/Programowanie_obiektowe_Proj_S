using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace po_lab04
{
    class Subject
    {
        private string _specialization;
        private int _semester;
        private int _hoursCount;

        public string Name { get; set; }
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Specjalizacja nie może być pusta ani mieć wartość null.");
                _specialization = value;
            }
        }
        public int Semester
        {
            get => _semester;
            set
            {
                if (value <= 0) throw new ArgumentException("Semestr musi być liczbą większą od 0");
                _semester = value;
            }
        }
        public int HoursCount
        {
            get => _hoursCount;
            set
            {
                if (value <= 0) throw new ArgumentException("Liczba godzin musi być większa od 0");
                _hoursCount = value;
            }
        }

        public Subject() : this(string.Empty, string.Empty, 1, 1) { }
        public Subject(string name, string specialization, int semester, int hoursCount) {
            this.Name = name;
            this.Specialization = specialization;
            this.Semester = semester;
            this.HoursCount = hoursCount;
        }

        public override string ToString()
        {
            return $"{Name}/{Specialization}/{Semester}/{HoursCount}";
        }

    }

    class FinalGrade
    {
        private Subject _subject;
        private double _value;

        public Subject Subject { get => _subject;
            set
            {
                if (value is null) throw new ArgumentNullException("Przedmiot nie może być pusty ani przyjmowac wartości null.");
                _subject = value;
            }

        }
        public double Value
        {
            get => _value;
            set
            {
                if (value < 2.0) throw new ArgumentException("Wartość oceny nie może być mniejsza od 2.0 ");
                _value = value;
            }
        }
        public DateTime Date { get; set; }

        public FinalGrade() : this(new Subject(), 2.0, DateTime.MinValue) { }
        public FinalGrade(Subject subject, double value, DateTime date)
        {
            Subject = subject;
            Value = value;
            Date = date;
        }

        public override string ToString()
        {
            return $"Ocena końcowa: {Subject}/{Date.ToShortDateString()}/{Value}";
        }
    }

    public interface IClassWithIList { }


    public abstract class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        public Person() { }
        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"Osoba: {FirstName} {LastName}/{DateOfBirth.ToShortDateString()}";
        }
    }

    class Lecturer : Person
    {
        public string AcademicTitle { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public Lecturer() : base() { }
        public Lecturer(string firstName, string lastName, DateTime dateOfBirth, string academicTitle, string position)
            : base(firstName, lastName, dateOfBirth)
        {
            AcademicTitle = academicTitle;
            Position = position;
        }

        public override string ToString()
        {
            return base.ToString() + $"/{AcademicTitle}/{Position}";
        }
    }

    class Student : Person, IClassWithIList
    {
        public static int _id;
        public static int Id => _id;

        public IList<FinalGrade> Grades { get; set; } = new List<FinalGrade>();
        public string Specialization { get; set; }

        public int _group;
        public int Group
        {
            get => _group;
            set
            {
                if (value <= 0) throw new ArgumentException("Wartość grupy musi być większa od 0.");
                _group = value;
            }
        }

        public int Semester { get; set; }
        public int IndexId { get; set; } = 0;
        public double AverageGrades { get => Grades.Average(g => g.Value); }

        public Student() : base()
        {
            _id++;
        }
        public Student(string firstName, string lastName, DateTime dateOfBirth, string specialization, int group, int semester) : base(firstName, lastName, dateOfBirth)
        {
            Specialization = specialization;
            Group = group;
            Semester = semester;
        }

        public override string ToString()
        {
            string s = Grades.Any()
                ? string.Join("\n", Grades)
                : "brak ocen";
            return $"{base.ToString()}/{Specialization}/{Group}/{Semester}/Oceny: {s}";
        }
    }

    class OrganizationUnit
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public IList<Lecturer> Lecturers { get; set; } = new List<Lecturer>();

        public OrganizationUnit() { }
        public OrganizationUnit(string name, string address, IList<Lecturer> lecturers) {
            Name = name;
            Address = address;
            Lecturers = lecturers;
        }

        public override string ToString()
        {
            return $"Jednostka: {Name}/{Address}/\n{string.Join("\n", Lecturers)}";
        }
    }

    class Department : IClassWithIList
    {
        public string Name { get; set; } = string.Empty;
        public Person Dean { get; set; }
        public IList<OrganizationUnit> OrganisationUnits { get; set; } = new List<OrganizationUnit>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<Student> Students { get; set; } = new List<Student>();

        public Department() { }
        public Department(string name, Person dean, IList<Subject> subjects, IList<Student> students)
        {
            Name = name;
            Dean = dean;
            Subjects = subjects;
            Students = students;
        }

        public override string ToString()
        {
            string j = OrganisationUnits.Any()
                ? string.Join("\n", OrganisationUnits)
                : "brak jednostek";

            string p = string.Join("\n", Subjects);
            string s = string.Join("\n", Students);
            return $"{Name}/{Dean} \n Jednostki: {j} \n Przedmioty: {p} Studenci: {s} \n";
        }
    }


    static class ClassWithListGetListExtension
    {
        public static IList<T>? GetList<T>(this IClassWithIList obj)
        {
            if (obj is null) return null;

            var property = obj
                .GetType()
                .GetProperties()
                .FirstOrDefault(p =>
                p.PropertyType == typeof(IList<T>));

            if (property is null) return null;

            return property.GetValue(obj) as IList<T>;
        }
    }

    static class ClassWithIListCrudOperationsExtension
    {
        public static void Add<T>(this IClassWithIList obj, T itemToAdd)
        {
            if (obj is null) return;

            var collection = obj.GetList<T>();
            if (collection is null) return;

            collection.Add(itemToAdd);
        }

        public static void AddRange<T>(this IClassWithIList obj, IList<T> itemsToAdd)
        {
            if (obj is null) return;

            var collection = obj.GetList<T>();
            if (collection is null) return;

            foreach (var item in itemsToAdd)
                collection.Add(item);
        }

        public static void Clear<T>(this IClassWithIList obj)
        {
            if (obj is null) return;

            var collection = obj.GetList<T>();
            if (collection is null) return;

            collection.Clear();
        }

        public static bool Contains<T>(this IClassWithIList obj, T itemToCheck)
        {
            if (obj is null) return false;

            var collection = obj.GetList<T>();
            if (collection is null) return false;

            return collection.Contains(itemToCheck);
        }

        public static int IndexOf<T>(this IClassWithIList obj, T itemToFind)
        {
            if (obj is null) return -1;

            var collection = obj.GetList<T>();
            if (collection is null) return -1;

            return collection.IndexOf(itemToFind);
        }

        public static bool Remove<T>(this IClassWithIList obj, T itemToRemove)
        {
            if (obj is null) return false;

            var collection = obj.GetList<T>();
            if (collection is null) return false;

            return collection.Remove(itemToRemove);
        }

        public static bool RemoveAt<T>(this IClassWithIList obj, int index)
        {
            if (obj is null) return false;

            var collection = obj.GetList<T>();
            if (collection is null) return false;

            if (index < 0 || index >= collection.Count) return false;

            collection.RemoveAt(index);

            return true;
        }

        public static void Update<T>(this IClassWithIList obj, T oldItem, T newItem)
        {
            if (obj is null || newItem is null) return;

            var collection = obj.GetList<T>();
            if (collection is null) return;

            int index = collection.IndexOf(oldItem);
            collection[index] = newItem;
        }

        public static void ForEach<T>(this IClassWithIList obj, Action<T> action)
        {
            if (obj is null || action is null) return;

            var collection = obj.GetList<T>();
            if (collection is null) return;

            foreach (var e in collection) action(e);
        }

        public static void RemoveAll<T>(this IClassWithIList obj, Func<T, bool> predicate)
        {
            if (obj is null) return;

            var collection = obj.GetList<T>();
            if (collection is null) return;

            var toRemove = collection.Where(predicate).ToList();

            foreach (var item in toRemove)
                collection.Remove(item);
        }
    }


    public static class ClassWithIListGroupingExtension
    {

        public class GroupReport<TKey, TElement>
        {
            public TKey Key { get; set; }
            public IList<TElement> Elements { get; set; }

            public override string ToString()
            {
                var elementsStrings = Elements.Select(item => $"    - {item}");
                var elementsBlock = string.Join("\n", elementsStrings);
                return $"  Grupa: {Key}\n{elementsBlock}";
            }
        }

        public class GroupReport<TOuterKey, TInnerKey, TElement>
        {
            public TOuterKey Key { get; set; }
            public IList<GroupReport<TInnerKey, TElement>> Elements { get; set; }

            public override string ToString()
            {
                var innerGroupsBlock = string.Join("\n", Elements);
                return $"Grupa Zewnętrzna: {Key}\n{innerGroupsBlock}";
            }
        }

        public static List<GroupReport<TKey, T>> GroupBy<T, TKey>(
            this IClassWithIList obj,
            Func<T, TKey> groupingKey)
        {
            var list = obj.GetList<T>();
            if (list == null || groupingKey == null)
            {
                return new List<GroupReport<TKey, T>>();
            }

            return list.GroupBy(groupingKey)
                       .Select(g => new GroupReport<TKey, T>
                       {
                           Key = g.Key,
                           Elements = g.ToList()
                       })
                       .ToList();
        }

        public static List<GroupReport<TOuterKey, TInnerKey, T>> GroupBy<T, TOuterKey, TInnerKey>(
            this IClassWithIList obj,
            Func<T, TOuterKey> outerKeySelector,
            Func<T, TInnerKey> innerKeySelector)
        {
            var list = obj.GetList<T>();
            if (list == null || outerKeySelector == null || innerKeySelector == null)
            {
                return new List<GroupReport<TOuterKey, TInnerKey, T>>();
            }

            return list.GroupBy(outerKeySelector)
                       .Select(outerGroup => new GroupReport<TOuterKey, TInnerKey, T>
                       {
                           Key = outerGroup.Key,
                           Elements = outerGroup.GroupBy(innerKeySelector)
                                                .Select(innerGroup => new GroupReport<TInnerKey, T>
                                                {
                                                    Key = innerGroup.Key,
                                                    Elements = innerGroup.ToList()
                                                })
                                                .ToList()
                       })
                       .ToList();
        }
    }






class Program
    {
        static void Main()
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
            new(soop, 5.0, new(2075,6,1)),
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

            Console.WriteLine($"[] Listy ocen po zainicjowaniu:\n" +
                              $"{string.Join("\n", grades1)}\n" +
                              $"{string.Join("\n", grades2)}\n" +
                              $"{string.Join("\n", grades3)}\n");


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
            Console.WriteLine("[] Grupowanie ocen s1 po nazwie przedmiotu:");
            var grupyA = s1.GroupBy<FinalGrade, string>(g => g.Subject.Name);
            foreach (var g in grupyA) Console.WriteLine(g);
            Console.WriteLine();

            Console.WriteLine("[] Grupowanie ocen s3 po wartości i nazwie przedmiotu:");
            var grupyB = s3.GroupBy<FinalGrade, double, string>(
            g => g.Value,
            g => g.Subject.Name
            );
            foreach (var g in grupyB) Console.WriteLine(g);
            Console.WriteLine();

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
        }
    }

}
