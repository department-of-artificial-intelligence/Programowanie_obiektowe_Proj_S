using System;

namespace CinemaNetworkManagement.Model
{
    public class Employment
    {
        public int Id { get; }
        public Employee Employee { get; }
        public Cinema Cinema { get; }
        public string Position { get; }
        public decimal Salary { get; }
        public DateTime StartDate { get; }
        public DateTime? EndDate { get; private set; }

        public bool IsActive           // Zatrudnianie jest aktywne jeœli nie ustawiono daty jego zakonczenia       -----------------------------------------------
        {
            get { return EndDate == null; }
        }

        public Employment(int id, Employee employee, Cinema cinema, string position, decimal salary, DateTime startDate)
        {
            Id = id;
            Employee = employee;
            Cinema = cinema;
            Position = position;
            Salary = salary;
            StartDate = startDate;
            EndDate = null;
        }

        
    }
}
