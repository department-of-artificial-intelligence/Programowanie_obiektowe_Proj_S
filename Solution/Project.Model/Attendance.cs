using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Attendance
    {
        public required int AttendanceId { get; set; }
        public required int StudentId {  get; set; }
        public required int GroupId {  get; set; }
        public required DateTime Date { get; set; }
        public required bool IsPresent {  get; set; }
        public required string Notes {  get; set; }

        public required Student Student { get; set; }
        public required Group Group { get; set; }
    }
}
