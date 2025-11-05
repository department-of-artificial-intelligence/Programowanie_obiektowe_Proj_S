using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Progress
    {
        public DateTime Date { get; set; }
        public float Weight_Progress { get; set; }
        public float WaistCircumference { get; set; }
        public float ArmCircumference { get; set; }
        public float Chest_Circumference { get; set; }

        public string Comentar { get; set; }
    }
}
