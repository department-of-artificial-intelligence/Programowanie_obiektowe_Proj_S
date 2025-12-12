using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
  public class Projekt()
  {
     public int Id { get; set; }
     public string Name { get; set; }
     public string Status { get; set; }
     public int Ocena { get; set; }
     public string Wlasciciel { get; set; } 
  }
}