using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public abstract class Person: IReportable
    {
        public required int Id { get; set;} 
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }

        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                if(DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                {
                    age--;
                }
                return age;
            }
        }

        public virtual string GetInfo()
        {
            return $"{FirstName} {LastName} (ID: {Id})";
        }
    }
}
