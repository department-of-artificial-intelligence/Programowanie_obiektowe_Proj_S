using Projekt;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        var pharmacy = new Pharmacy("Аптека №1", "Улица Ленина, 10");

       
        pharmacy.AddEmployee(new Employee(1, "Иван Иванов", "Фармацевт", "89012345678"));
        pharmacy.AddEmployee(new Employee(2, "Мария Петрова", "Кассир", "89098765432"));

        
        pharmacy.AddProduct(new Product(1, "Aspirin", 10, 100));
        pharmacy.AddProduct(new Product(2, "Paracetamol", 15, 50));

        
        Console.WriteLine("Продукты в аптеке:");
        foreach (var product in pharmacy.Products)
        {
            Console.WriteLine($"{product.Name} - {product.Quantity} шт. по {product.Price} руб.");
        }

        
        var sale = new Sale(1, pharmacy.Employees[1]); 
        var saleItem1 = new SaleItem(pharmacy.Products[0], 2);
        var saleItem2 = new SaleItem(pharmacy.Products[1], 1); 

        sale.AddItem(saleItem1);
        sale.AddItem(saleItem2);

        
        pharmacy.Products[0].DecreaseQuantity(2);
        pharmacy.Products[1].DecreaseQuantity(1);

        pharmacy.RecordSale(sale);

        
        Console.WriteLine($"Sprzedaz №{sale.Id} - {sale.SaleDate}");
        Console.WriteLine($"Sprzedający: {sale.Seller.FullName} ({sale.Seller.Position})");
        Console.WriteLine("Sprzedano:");

        
        Console.WriteLine($"{sale.ToString()}");


        LINQ l=new LINQ();
        Console.WriteLine($"{ l.Search(pharmacy.Products, "Paracetamol") }");
        
    }
}
