using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Pracodawca : public Pracownik
    {
        public required string B_FirstName {  get; set; }

        public required string B_LastName { get; set; }

        public required int B_Id { get; set; }

        public required int B_Age { get; set; }


        public required int Amountofemployees { get; set; }


        public void DoEmployeesProjectsCheck( )
        {
            Console.WriteLine(grade);
        }

        public void DoMeeting(string MeetingDate)
        {
            if(MeetingDate == "25.XX.20XX")
            {
                Console.WriteLine("Dzisiaj odbędzie sie ważne spotkanie o godz. 16:00")
            }
        }
    }

    


}
