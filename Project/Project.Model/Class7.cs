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
    }
}
