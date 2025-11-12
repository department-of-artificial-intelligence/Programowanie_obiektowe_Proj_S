using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Client : User
    {
        public required string LevelOfAdvanced { get; set; }
        public required int Goals { get; set; }
        public required float Weight { get; set; }
        public required float Height { get; set; }
        public required float Age { get; set; }
        public List<Plan> Plan { get; set; } = new List<Plan>(); 
        public List<string> History { get; set; } = new List<string>(); 

        public Client(string name, string lastName, int role, int contactDetails, string levelOfAdvanced, int goals, float weight, float height, float age)
            : base(name, lastName, role, contactDetails) 
        {
            LevelOfAdvanced = levelOfAdvanced;
            Goals = goals;
            Weight = weight;
            Height = height;
            Age = age;
        }
        public Client() : base() 
        {
            LevelOfAdvanced = string.Empty;
            Goals = 0;
            Weight = 0;
            Height = 0;
            Age = 0;
            Plan = new List<Plan>();
            History = new List<string>();
        }
    }
}
