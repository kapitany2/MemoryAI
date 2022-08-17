using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game.AI
{
    /// <summary>
     /// Jelenlegi lépés állapotát adja meg.
     /// </summary>
    public enum Allapot
    {
        ElsoValasztas,
        MasodikValasztas,
        Varakozik,
        Levesz,
        Visszafordit,
        Ellenoriz,
        Vege
    }
}
