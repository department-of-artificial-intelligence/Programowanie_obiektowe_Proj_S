using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public Wyplata() : public Pracownik
	{
	
		public required int Tittle { get; set; }

        public required string Date { get; set; }
        
        public required int Id { get; set; }
        
        public required string FirstName { get; set; }
        
        public required string LastName { get; set; }
        
        public required int Amount { get; set; }
        
        
        public void DoPayment(int Tittle, string Date, int Id, string FirstName, string LastName, int Amount)
        {
            if (Date == "16.XX.20XX")
            {
                Console.WriteLine(Tittle);
                Console.WriteLine(FirstName);
                Console.WriteLine(LastName);
                Console.WriteLine(Id);
                Console.WriteLine(Amount);

            }
            if(PassedProjects(amountofpassed) > NotPassedProjects(amountofnotpassed))
            {
                
                Console.WriteLine("Dodatkowa premia: ",  )
            }
        
        }
     
        

      }
}


     


}



