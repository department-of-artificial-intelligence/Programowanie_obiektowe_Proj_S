using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia.Model;

public class Wypozyczenie {
    public int Id { get; set; }
    public Klient Klient { get; set; } = null!;
    public Rower Rower { get; set; } = null!;
    public DateTime DataStart {  get; set; }
    public DateTime DataKonca {  get; set; }
}
