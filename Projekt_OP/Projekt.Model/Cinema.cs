using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Cinema
    {
        private int _cinemaID;
        private string _cinemaName;
        private List<Hall> _hall;
        private CinemaAddress _address;
        private List<Employee> _employees;

        public int CinemaID { get { return _cinemaID; } set { _cinemaID = value; } }

        public string CinemaName { get { return _cinemaName; } set { _cinemaName = value; } }

        public List<Hall> Hall { get { return _hall; } set { _hall = value; } }

        public CinemaAddress Address { get { return _address; } set { _address = value; } }

        public List<Employee> Empolyees { get { return _employees; } set { _employees = value; }  }

        public Cinema() 
        {
            _cinemaID = 0;
            _cinemaName = string.Empty;
            _address = new CinemaAddress();
            _hall = new List<Hall>();
            _employees = new List<Employee>();

        }
        

        public Cinema(int CinemaID,string CinemaName,CinemaAddress Address,List<Hall> Hall,List<Employee> Employees) 
        {
            this.CinemaID = CinemaID;
            this.CinemaName = CinemaName;
            this.Address = Address;
            this.Hall = Hall;
            this._employees  = Employees;
        }


        public override string ToString()
        {
            return $"|Kino: {CinemaName}--IDKina: {CinemaID}|\n" +
                $"------------------------------------------------";
        }
    }
}
