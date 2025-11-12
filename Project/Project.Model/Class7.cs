using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Training
    {
        public string Name { get; set; }
        public string TypeOfTrening { get; set; }
        public float TrainingDuration { get; set; }
        public string Notes { get; set; }
        public List<Exercise>Exercise { get; set; } = new List<Exercise>();
        public Training(string name, string typeOfTrening, float trainingDuration, string notes, List<Exercise> exercise)
        {
            Name = name;
            TypeOfTrening = typeOfTrening;
            TrainingDuration = trainingDuration;
            Notes = notes;
            Exercise = exercise ?? new List<Exercise>(); 
        }

        public Training()
        {
            Name = string.Empty;
            TypeOfTrening = string.Empty;
            TrainingDuration = 0;
            Notes = string.Empty;
            Exercise = new List<Exercise>(); 
        }
    }
}
    

