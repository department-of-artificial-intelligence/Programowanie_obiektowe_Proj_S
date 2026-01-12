using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Model.Orders; 

namespace Project.Logic.Extensions
{
    public static class StoreExtensions
    {
        
        public static string MaskSensitiveData(this string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            if (input.Length <= 4)
            {
                return "****";
            }
            else
            {
                return $"****-****-****-{input.Substring(input.Length - 4)}";
            }

               
        }

        
        public static string RemoveSpecialCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                return input.Replace("-", "").Replace(" ", "").Trim();
            }
                
        }

        
        public static bool HasItems<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        
        public static decimal CalculateTotalRevenue(this IEnumerable<Order> orders)
        {
            if (!orders.HasItems())
            {
                return 0;
            }
            else
            {
                return orders
                    .Where(o => o.Status != OrderStatus.Cancelled)
                    .Sum(o => o.GetTotalAmount());
            }

                
        }


        public static string ToConsoleString<T>(this IEnumerable<T> list)
        {
            if (!list.HasItems())
            {
                return "[Pusta lista]";
            }
            else
            {
                return string.Join(Environment.NewLine, list.Select(item => $" - {item}"));
            }
               
        }


        
        public static ConsoleColor ToConsoleColor(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.New => ConsoleColor.Yellow,         
                OrderStatus.Confirmed => ConsoleColor.White,    
                OrderStatus.Paid => ConsoleColor.Green,         
                OrderStatus.Shipped => ConsoleColor.Cyan,       
                OrderStatus.Completed => ConsoleColor.DarkGreen,
                OrderStatus.Cancelled => ConsoleColor.Red,      
                _ => ConsoleColor.Gray
            };
        }



        public static string ToPolishDescription(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.New => "Nowe (Oczekuje na płatność)",
                OrderStatus.Confirmed => "Potwierdzone",
                OrderStatus.Paid => "Opłacone",
                OrderStatus.Shipped => "Wysłane do klienta",
                OrderStatus.Completed => "Zakończone",
                OrderStatus.Cancelled => "Anulowane",
                _ => status.ToString()
            };
        }




        public static void PrintColored(this OrderStatus status)
        {
            var originalColor = Console.ForegroundColor; 

            Console.ForegroundColor = status.ToConsoleColor(); 
            Console.Write(status.ToPolishDescription());      

            Console.ForegroundColor = originalColor; 
        }
    }
}