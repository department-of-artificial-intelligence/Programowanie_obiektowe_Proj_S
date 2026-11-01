using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public class Employee : Person
    {
        private readonly List<Employment> _employmentHistory;

        public List<Employment> EmploymentHistory
        {
            get { return new List<Employment>(_employmentHistory); }
        }

        public bool IsEmployed
        {
            get
            {
                foreach (Employment employment in _employmentHistory)
                {
                    if (employment.IsActive)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public Employee(int id, string firstName, string lastName)
            : base(id, firstName, lastName)
        {
            _employmentHistory = new List<Employment>();
        }

        public override string GenerateDescription()
        {
            return $"Pracownik: {base.GenerateDescription()}, zatrudniony: {IsEmployed}";
        }

       
    }
}
