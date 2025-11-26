using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace woch_lab3
{
    class Person
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public Person() : this(string.Empty, string.Empty) { }
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }

    class Author : Person
    {
        public string Nationality { get; set; } = string.Empty;

        public Author() : base() { }

        public Author(string firstName, string lastName, string nationality)
            : base(firstName, lastName)
        {
            Nationality = nationality;
        }

        public override string ToString() => $"Autor: {base.ToString()} {Nationality}";
    }

    class Librarian : Person
    {
        public DateTime HireDate { get; set; } = DateTime.MinValue;
        public double Salary { get; set; } = 0;

        public Librarian() : base() { }

        public Librarian(string firstName, string lastName, DateTime hireDate, double salary)
            : base(firstName, lastName)
        {
            HireDate = hireDate;
            Salary = salary;
        }

        public override string ToString() => $"Bibliotekarz: {base.ToString()} {HireDate}/{Salary}";
    }

    interface IBarCodeGenerator
    {
        string GenerateBarCode();
    }

    abstract class Item
    {
        private int _id;

        public string Title { get; set; }
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "ID musi być > 0.");
                _id = value;
            }
        }
        public string? Publisher { get; set; }
        public DateTime DateOfIssue { get; set; }

        public Item()
        {
            Title = string.Empty;
            Id = 1;
            Publisher = string.Empty;
            DateOfIssue = DateTime.MinValue;
        }

        public Item(string title, int id, string publisher, DateTime dateOfIssue)
        {
            Title = title;
            Id = id;
            Publisher = publisher;
            DateOfIssue = dateOfIssue;
        }

        public override string ToString() => $"{Title}/{Id}/{Publisher}/{DateOfIssue}";

        public abstract string GenerateBarCode();
    }



    class Journal : Item
    {
        private int _number;

        public int Number
        {
            get => _number;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Numer musi być większy od 0.");
                _number = value;
            }
        }

        public Journal() : base()
        {
            Number = 1;
        }

        public Journal(string title, int id, string publisher, DateTime dateOfIssue, int number)
            : base(title, id, publisher, dateOfIssue)
        {
            Number = number;
        }

        public override string ToString()
        {
            return $"Czasopismo: {base.ToString()}/{Number}";
        }
        public override string GenerateBarCode()
        {
            return $"J-{Id}-{DateOfIssue}-{Number}";
        }
    }

    class Book : Item
    {
        private int _pageCount;
        public int PageCount
        {
            get => _pageCount;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Liczba stron nie może być ujemna.");
                _pageCount = value;
            }
        }

        public List<Author> Authors { get; set; } = new List<Author>();
        public List<string> Categories { get; set; } = new List<string>();

        public Book() : base()
        {
            this.PageCount = 0;
            this.Authors = new List<Author>();
            this.Categories = new List<string>();
        }

        public Book(string title, int id, string publisher, DateTime dateOfIssue, int pageCount, List<Author> authors)
            : base(title, id, publisher, dateOfIssue)
        {
            this.PageCount = pageCount;
            this.Authors = authors;
            this.Categories = new List<string>();
        }

        public void AddAuthor(Author author)
        {
            if (author is null) throw new ArgumentNullException(nameof(author));
            if (Authors.Contains(author)) throw new InvalidOperationException("Autor jest już na liście.");
            Authors.Add(author);
        }

        public void RemoveAuthor(Author author)
        {
            if (Authors.Contains(author)) Authors.Remove(author);
        }

        public void AddCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category)) throw new ArgumentNullException(nameof(category));
            if (!Categories.Contains(category)) Categories.Add(category);
        }

        public override string ToString()
        {
            string authorsText = string.Join("\n", Authors);
            return $"Książka: {base.ToString()}/{PageCount}/{authorsText}";
        }

        public override string GenerateBarCode()
        {
            return $"B-{Id}-{DateOfIssue}-{Publisher}";
        }
    }


    interface IItemManagement
    {
        List<Item> Items { get; set; }
        string GetAllItems(string s = "");

        Item? FindItemBy(int id);
        Item? FindItemBy(string title);
        Item? FindItem(Func<Item, bool> predicate);
    }

    class Catalog : IItemManagement
    {

        public List<string> Categories { get; set; } = new List<string>();
        public string ThematicDepartment { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

        public Catalog (List<Item> items) {
            this.Items = items ?? new List<Item>();
            this.ThematicDepartment = string.Empty;
        }
        public Catalog(string thematicDepartment, List<Item> items) : this(items) { 
            this.ThematicDepartment=thematicDepartment;
        }

        public void AddCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentNullException(nameof(category), "Kategoria nie może być pusta.");

            if (!Categories.Contains(category))
                Categories.Add(category);
        }

        public void RemoveCategory(string category)
        {
            if (Categories.Contains(category))
                Categories.Remove(category);
        }

        public List<Book> FilterBooksByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentNullException(nameof(category));

            return Items
                .OfType<Book>()
                .Where(b => b.Categories.Contains(category))
                .ToList();
        }

        public void AddItem(Item item)
        {
            if(this.Items.Contains(item)) return;
            this.Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if(this.Items.Contains(item)) this.Items.Remove(item);
        }

        public override string ToString()
        {
            string s = (Items.Count > 0)
                ? string.Join('\n', Items)
                : "Brak pozycji w katalogu.";
            return $"Katalog: {ThematicDepartment}/{s}";
        }

        public string GetAllItems(string s = "")
        {
            if (Items == null || Items.Count == 0)
                return "Brak elementów w katalogu.";

            return string.Join(s, Items);
        }

        public Item? FindItemBy(int id)
        {
            if (Items is null) throw new InvalidOperationException("Nie można wyszukać elementu po indeksie - kolekcja Items nie została zainicjalizowana");

            var item = Items.FirstOrDefault(i => i.Id == id);
            return item;
        }

        public Item? FindItemBy(string title) // ==============dodana funkcjonalność -> ignoruje wielkość liter
        {
            if (Items is null) throw new InvalidOperationException("Nie można wyszukać elementu po tytule - kolekcja Items nie została zainicjalizowana");

            var item = Items.FirstOrDefault(i => i.Title.ToLower() == title.ToLower());
            return item;
        }

        public Item? FindItem(Func<Item, bool> predicate)
        {
            if (Items is null)
                throw new InvalidOperationException("Nie można wyszukać elementu - kolekcja Items nie została zainicjalizowana");
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predykat nie może być pusty.");

            return Items.FirstOrDefault(predicate);
        }

        public Item? FindItemWithExpression(Expression<Func<Item, bool>> expression)
        {
            if (Items is null)
                throw new InvalidOperationException("Nie można wyszukać elementu - kolekcja Items nie została zainicjalizowana.");
            if (expression == null)
                throw new ArgumentNullException(nameof(expression), "Wyrażenie nie może być puste.");

            var predicate = expression.Compile();

            return Items.FirstOrDefault(predicate);
        }
    }

    class Library : IItemManagement
    {
        public string Address { get; set; } = string.Empty;
        public List<Librarian> Librarians { get; set; } = new List<Librarian>();
        public List<Catalog> Catalogs { get; set; } = new List<Catalog>();
        public List<Item> Items
        {
            get => Catalogs.SelectMany(c => c.Items).ToList();
            set { }
        }

        public Library() { }
        public Library(string address, List<Librarian> librarians, List<Catalog> catalogs)
        {
            Address = address;
            Librarians = librarians ?? new List<Librarian>();
            Catalogs = catalogs ?? new List<Catalog>();
        }

        public void AddLibrarian(Librarian librarian)
        {
            if (librarian is null) throw new ArgumentNullException(nameof(librarian), "Bibliotekarz nie może mieć wartości pustej.");
            if (Librarians.Contains(librarian)) throw new InvalidOperationException("Bibliotekarz jest już na liście.");
            Librarians.Add(librarian);
        }

        public void RemoveLibrarian(Librarian librarian)
        {
            if (Librarians.Contains(librarian)) Librarians.Remove(librarian);
        }

        public string GetAllLibrarians(string s = "")
        {
            if (Librarians is null || Librarians.Count == 0) throw new ArgumentNullException("Brak bibliotekarzy na liście.");
            return string.Join(s, Librarians);
        }

        public void AddCatalog(Catalog catalog)
        {
            if (catalog is null) throw new ArgumentNullException(nameof(catalog), "Katalog jest pusty.");
            if (Catalogs.Contains(catalog)) throw new InvalidOperationException("Katalog znajduje się już na liście.");
            Catalogs.Add(catalog);
        }

        public void RemoveCatalog(Catalog catalog)
        {
            if (Catalogs.Contains(catalog)) Catalogs.Remove(catalog);
        }

        public void AddItem(Item item, string thematicDepartment)
        {
            var catalog = Catalogs.FirstOrDefault(c => c.ThematicDepartment == thematicDepartment);
            if (catalog is null)
                throw new InvalidOperationException("Nie znaleziono katalogu o podanym dziale tematycznym.");

            catalog.AddItem(item);
        }


        public string GetAllItems(string s = "")
        {
            if (Catalogs is null || Catalogs.Count == 0)
                throw new ArgumentNullException("Brak katalogów w bibliotece.");

            return string.Join(s, Catalogs);
        }

        public Item? FindItemBy(int id)
        {
            if (Catalogs is null) throw new InvalidOperationException("Nie można wyszukać elementu po indeksie - kolekcja Catalogs nie została zainicjalizowana");
            return Catalogs.SelectMany(c => c.Items)
                           .FirstOrDefault(i => i.Id == id);
        }

        public Item? FindItemBy(string title)
        {
            if (title == null)
            {
                return null;
            }
            string lowerTitle = title.ToLower();
            return Catalogs.SelectMany(c => c.Items)
                           .FirstOrDefault(i => i.Title.ToLower() == lowerTitle);
        }

        public Item? FindItem(Func<Item, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            return Catalogs.SelectMany(c => c.Items).FirstOrDefault(predicate);
        }


        public override string ToString()
        {
            return $"Biblioteka: {Address} (Katalogi: {Catalogs.Count}, Bibliotekarze: {Librarians.Count})";
        }
    }
      

    class GroupedItemsByKeyReport<TKey, TValue>
    {
        public TKey Key {  get; set; }
        public List<TValue> Items { get; set; }

        public GroupedItemsByKeyReport()
        {
            Key = default;
            Items = new List<TValue>();
        }

        public GroupedItemsByKeyReport(TKey key, List<TValue> items)
        {
            Key = key;
            Items = items ?? new List<TValue>();
        }

        public override string ToString()
        {
            string s = Items != null && Items.Count > 0
                ? string.Join("; ", Items)
                : "Brak pozycji";

            return $"Klucz grupy: {Key}/liczba pozycji: {Items.Count}/pozycje: {s}";
        }
    }

    static class GroupItemsHelper
    {

        public static List<GroupedItemsByKeyReport<TKey, TValue>>? GroupItemsBy<TKey, TValue>(
            IEnumerable<TValue> items,
            Func<TValue, TKey> keySelector)
        {
            var groupedItems = items
            .GroupBy(keySelector)
                .Select(g => new GroupedItemsByKeyReport<TKey, TValue>(g.Key, g.ToList()))
                .ToList();
            return groupedItems;
        }
    }






    class Program
    {
        static void Main()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy operacji podstawowych");
            Console.WriteLine("-----------------------------------------------------------");
            Author a1 = new("Anna", "Tracz", "polska");
            Author a2 = new("Jan", "Bielik", "polska");
            Author a3 = new("Maria", "Miłka", "polska");
            Item i1 = new Book("Kompendium programisty", 2, "PolPress", new
            DateTime(2065, 12, 06), 500, [a1, a2]);
            ((Book)i1).AddAuthor(a3);
            Item i4 = new Book("Kompendium administratora baz danych", 3, "PolPress", new
            DateTime(2065, 05, 01), 500, [a3]);
            ((Book)i4).AddAuthor(a1);
            Item i2 = new Journal("Przegląd techniczny", 1, "MyPress", new
            DateTime(2060, 06, 01), 1);
            var bookBarCode = ((Book)i1).GenerateBarCode();
            Console.WriteLine($"{i1} \r\n  Kod kreskowy: {bookBarCode}");
            var journalBarCode = ((Journal)i2).GenerateBarCode();
            Console.WriteLine($"{i2} \r\n  Kod kreskowy: {journalBarCode}");
            List<Item> items1 = [i1, i2, i4];
            Catalog c1 = new("Książki o programowaniu", items1);
            c1.AddItem(new Journal("Wzorce programistyczne", 4, "ITPress", new
            DateTime(2060, 02, 14), 1));
            Console.WriteLine(c1);
            Console.WriteLine('\n' + c1.GetAllItems("Wszystkie pozycje w katalogu:"));


            
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy wyszukiwania przedmiotów");
            Console.WriteLine("-----------------------------------------------------------");
            string searchedValue = "Kompendium programisty";
            Item? foundedItemByTitle = c1?.FindItem(item => item.Title == searchedValue);
            Item? foundedItemById = c1?.FindItem(item => item.Id == 1);
            Item? foundedItemByDateRange = c1?.FindItem(
            item => item.DateOfIssue >= new DateTime(2055, 01, 01) &&
            item.DateOfIssue <= new DateTime(2085, 12, 31)
            );
            Console.WriteLine("Wyszukanie po id:\n" + foundedItemById);
            Console.WriteLine("Wyszukanie po tytule:\n" + foundedItemByTitle);
            Console.WriteLine("Wyszukanie po datach:\n" + foundedItemByDateRange);
            Item? foundedItemByIdOld = c1?.FindItemBy(1);
            Item? foundedItemByTitleOld = c1?.FindItemBy(searchedValue);
            Console.WriteLine("Wyszukanie po id (wersja 2):\n" + foundedItemByIdOld);
            Console.WriteLine("Wyszukanie po tytule (wersja 2):\n" + foundedItemByTitleOld);


            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy dla bibliotek");
            Console.WriteLine("-----------------------------------------------------------");
            Person l1 = new Librarian("Maja", "Kowal", DateTime.Now.Date, 2040);
            Person l2 = new Librarian("Jan", "Morus", DateTime.Now.Date, 2040);
            Library lib1 = new("Częstochowa, Armii Krajowej 36", [(Librarian)l1], []);
            lib1.AddLibrarian((Librarian)l2);
            Console.WriteLine(lib1.GetAllLibrarians("\nWszyscy bibliotekarze:"));
            Catalog c2 = new("Powieści", []);
            lib1.AddCatalog(c2);
            if (c1 != null) lib1.AddCatalog(c1);
            Item i3 = new Book("Głos większości", 5, "Nasze wersy", new
            DateTime(2061, 03, 08), 800, [a1]);
            lib1.AddItem(i3, "Powieści");
            Console.WriteLine(lib1);
            Console.WriteLine(lib1.GetAllItems("\nWszystkie pozycje w bibliotece:"));
            var foundedById = lib1.FindItemBy(4);
            var foundedByTitle = lib1.FindItemBy(searchedValue);
            var foundedByLambda = lib1.FindItem(x => x.Publisher == "ITPress");
            Console.WriteLine(foundedById);
            Console.WriteLine(foundedByTitle);
            Console.WriteLine(foundedByLambda);


            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy dla dodawania i usuwania przedmiotów");
            Console.WriteLine("-----------------------------------------------------------");
            Catalog c3 = new("Literatura fantastyczna", []);
            Library lib2 = new("Warszawa, Marszałkowska 12", [], [c3]);
            var bookToAdd = new Book("Lot ku centrum", 8, "Super Press", new
            DateTime(2060, 06, 01), 350, [a1]);
            c3.AddItem(bookToAdd);
            Console.WriteLine("Po dodaniu książki:");
            Console.WriteLine(c3.GetAllItems("Pozycje w katalogu:"));
            c3.RemoveItem(bookToAdd);
            Console.WriteLine("Po usunięciu książki:");
            Console.Write(c3.GetAllItems("Pozycje w katalogu:"));


            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("\nTesty wyszukiwania z bardziej złożonymi warunkami");
            Console.WriteLine("-----------------------------------------------------------");
            var foundByMultipleConditions =
            lib1?.FindItem(x => x.Publisher == "ITPress" && x.DateOfIssue >= new
            DateTime(2040, 01, 01));
            Console.WriteLine("Wyszukiwanie po wielu warunkach:");
            Console.WriteLine(foundByMultipleConditions);
            Console.WriteLine("Pozycja zawierająca w tytule 'ę':");
            Console.WriteLine(lib1?.Catalogs[0].FindItem(c =>
            c.Title.Contains('ę'))?.ToString());
            Console.WriteLine("Pozycja zawierająca w tytule 'ę' (z Expression):");
            Console.WriteLine(lib1?.Catalogs[0].FindItemWithExpression(c =>
            c.Title.Contains('ę'))?.ToString());
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy dla błędnych argumentów");
            Console.WriteLine("-----------------------------------------------------------");
            Item? nonExistentItem = lib1?.FindItemBy(999);
            Console.WriteLine("Wyszukiwanie nieistniejącego przedmiotu (ID 999):");
            Console.WriteLine(nonExistentItem?.ToString() ?? "Nie znaleziono przedmiotu");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy grupowania po roku wydania (metoda generyczna)");
            Console.WriteLine("-----------------------------------------------------------");
            lib1 ??= new();
            List<GroupedItemsByKeyReport<int, Item>>? groupedByYear =
            GroupItemsHelper.GroupItemsBy(
            lib1.Catalogs.SelectMany(c => c.Items).ToList(),
            item => item.DateOfIssue.Year
            );
            if (groupedByYear is not null) foreach (var e in
            groupedByYear) Console.WriteLine(e);
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy grupowania po wydawcy (metoda generyczna)");
            Console.WriteLine("-----------------------------------------------------------");
            List<GroupedItemsByKeyReport<string, Item>>? groupedByPublisher =
            GroupItemsHelper.GroupItemsBy(
            lib1.Catalogs.SelectMany(c => c.Items).ToList(),
            item => item.Publisher
            );
            if (groupedByPublisher is not null) foreach (var e in groupedByPublisher)
                    Console.WriteLine(e);
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy grupowania książek po liczbie stron (m. generyczna)");
            Console.WriteLine("-----------------------------------------------------------");
            List<GroupedItemsByKeyReport<int, Book>>? groupedByPageCount =
            GroupItemsHelper.GroupItemsBy(
            lib1.Catalogs.SelectMany(c => c.Items).OfType<Book>().ToList(),
            item => item.PageCount
            );
            if (groupedByPageCount is not null) foreach (var e in groupedByPageCount)
                    Console.WriteLine(e);
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy grupowania autorów po narodowości (m. generyczna)");
            Console.WriteLine("-----------------------------------------------------------");
            List<GroupedItemsByKeyReport<string, Author>>? groupedByNationality =
            GroupItemsHelper.GroupItemsBy(
            lib1.Catalogs
            .SelectMany(c => c.Items)
            .OfType<Book>()
            .SelectMany(b => b.Authors)
            .Distinct()
            .ToList(),
            item => item.Nationality
            );
            if (groupedByNationality is not null) foreach (var e in groupedByNationality)
                    Console.WriteLine(e);


            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Testy filtrowania książek po tematyce");
            Console.WriteLine("-----------------------------------------------------------");
            ((Book)i1).AddCategory("programowanie");
            ((Book)i1).AddCategory("algorytmy");

            ((Book)i4).AddCategory("bazy danych");
            ((Book)i4).AddCategory("programowanie");

            ((Book)i3).AddCategory("fantastyka");
            ((Book)i3).AddCategory("powieść");

            Console.WriteLine("Książki z kategorii 'programowanie':");
            foreach (var b in c1.FilterBooksByCategory("programowanie"))
                Console.WriteLine(b);

            Console.WriteLine("\nKsiążki z kategorii 'algorytmy':");
            foreach (var b in c1.FilterBooksByCategory("algorytmy"))
                Console.WriteLine(b);

            Console.WriteLine("\nKsiążki z kategorii 'bazy danych':");
            foreach (var b in c1.FilterBooksByCategory("bazy danych"))
                Console.WriteLine(b);


        }
    }
}
