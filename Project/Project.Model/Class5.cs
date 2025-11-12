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

        public Progress(DateTime date, float weightProgress, float waistCircumference, float armCircumference, float chestCircumference, string comentar)
        {
            Date = date;
            Weight_Progress = weightProgress;
            WaistCircumference = waistCircumference;
            ArmCircumference = armCircumference;
            Chest_Circumference = chestCircumference;
            Comentar = comentar;
        }
        public Progress()
        {
            Date = DateTime.Now;
            Weight_Progress = 0;
            WaistCircumference = 0;
            ArmCircumference = 0;
            Chest_Circumference = 0;
            Comentar = string.Empty; 
        }
    }
}
