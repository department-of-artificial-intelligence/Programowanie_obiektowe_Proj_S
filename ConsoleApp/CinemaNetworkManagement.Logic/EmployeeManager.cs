using CinemaNetworkManagement.Model;

namespace CinemaNetworkManagement.Logic
{
    public class EmployeeManager
    {
        private readonly ISourceEmployee _employees;
        private readonly ICinemasSource _cinemas;

        public EmployeeManager(
            ISourceEmployee employees,
            ICinemasSource cinemas)
        {
            _employees = employees;
            _cinemas = cinemas;
        }

    }
}
