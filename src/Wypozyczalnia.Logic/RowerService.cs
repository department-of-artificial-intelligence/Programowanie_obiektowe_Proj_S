using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Wypozyczalnia.Logic.Interfaces;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Logic;
public class RowerService : IRowerService {
    public bool CzyDostepny(Rower rower) {
        return rower.Status == StatusRoweru.Dostepny;
    }

    public void ZmienStatus(Rower rower, StatusRoweru nowyStatus) {
        rower.Status = nowyStatus;
    }
}
