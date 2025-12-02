using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.DTOs
{
    public class GroupStatDto
    {
        public string GroupName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public int MaxStudents { get; set; }
        public double FillPercent { get; set; }
    }
}
