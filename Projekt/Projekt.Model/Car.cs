using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        private string RegistrationNumber { get; set; }
        public int Mileage { get; set; }
        public decimal DailyRate { get; set; }
        public CarStatus Status { get; set; }
        public int CurrentBranchId { get; set; }
        public virtual Branch CurrentBranch { get; set; }
    }
}
