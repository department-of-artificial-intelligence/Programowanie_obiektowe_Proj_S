using System;
using System.Linq;
using Projekt.Model;


namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var cinemaMenager = new CinemaMenager();

            bool isRunning = true;

            Console.WriteLine(" Witamy w Systemie Zarzadzania Kinami! ");
            Console.WriteLine("---------------------------------------");

            

            Cinema kino1 = new Cinema(1,"Kino1",new CinemaAddress("Cukierkowa")  );


        }
    }
}
    




