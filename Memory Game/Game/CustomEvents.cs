using System;

namespace Memory_Game.Game
{
    public class GameEventArgs : EventArgs
    {
        public bool Nyert { get; set; }
        public GameEventArgs(bool nyert)
        {
            Nyert = nyert;
        }
    }

    public class LapForditasEventArgs : EventArgs
    {
        public int Index { get; set; }
        public int HatterIndex { get; set; }
        public LapForditasEventArgs(int index, int hatterIndex)
        {
            Index = index;
            HatterIndex = hatterIndex;
        }
    }

    public class LapLeveszEventArgs : EventArgs
    {
        public int Sorszam { get; set; }
        public int Ertek { get; set; }
        public LapLeveszEventArgs(int sorszam, int ertek)
        {
            Sorszam = sorszam;
            Ertek = ertek;
        }
    }
}
