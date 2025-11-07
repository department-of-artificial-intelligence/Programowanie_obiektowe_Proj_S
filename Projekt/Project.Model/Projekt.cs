using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;
{
  public class Projekt()
  {
     public string Name { get; set; }
     public string Status { get; set; }
     public int Ocena { get; set; }
     public string Wlasciciel { get; set; } 
  }
}