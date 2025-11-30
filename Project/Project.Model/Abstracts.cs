using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model;

public interface IPlayManager
{
    bool AddPlay(Play play);
    bool RemovePlay(Play play);
    bool RemovePlay(int playId);
    void RemoveAllPlays();
}
