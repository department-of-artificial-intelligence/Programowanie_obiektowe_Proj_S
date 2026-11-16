using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Logic.Interfaces;

public interface IRowerService {
    void ZmienStatus(Rower rower, StatusRoweru nowyStatus);
    bool CzyDostepny(Rower rower);
}
