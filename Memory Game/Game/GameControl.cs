using Memory_Game.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game
{
    public partial class GameControl : UserControl
    {
        internal int LevettParokSzama { get; set; }
        internal int elfelejtettLapok { get; set; }
        internal int vakTalalalat { get; set; }

        public event EventHandler JatekVegeEsemeny;
        public event EventHandler LapForditottEsemeny;
        public event EventHandler LapLevesz;
        public event EventHandler Korvege;
        public event EventHandler Elkezdodott;
        public event EventHandler FrissitIdo;

        internal int lapokSzama;
        internal int sorokSzama;
        internal bool tartAJatek;
        internal DateTime kezdoIdo;
        Random rnd = new Random();
        FlowLayoutPanel panel;

        List<int> sorszamok;

        public GameControl()
        {
            InitializeComponent();
            //Lap.MasodikLapFelforditva += (send, args) =>
            //{
            //    timerFordit.Enabled = true;
            //};
            BackColor = Color.Transparent;
            panel = new FlowLayoutPanel();
            this.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
            sorszamok = new List<int>();
            sorokSzama = 2;
        }
        private void GameControl_Load(object sender, EventArgs e)
        {
            Init();
        }

        public void Init()
        {
            tartAJatek = false;
            LevettParokSzama = elfelejtettLapok = vakTalalalat = 0;
            sorszamSorsol();
            sorszamKever();
            panel.Controls.Clear();
            GC.Collect();
            int index = 0;
            foreach (var sorszam in sorszamok)
            {
                Lap lap = new Lap(sorszam, Width / sorokSzama)
                {
                    Name = index.ToString()
                };
                lap.LapKattintas += (send, args) =>
                {
                    if (!tartAJatek)
                    {
                        tartAJatek = true;
                        kezdoIdo = DateTime.Now;
                        timer.Enabled = true;
                        timer.Start();
                        Elkezdodott(kezdoIdo, EventArgs.Empty);
                    }
                    LapForditottEsemeny(send, args);
                };
                lap.MasodikLapFelforditva += (send, args) =>
                {
                    timerFordit.Enabled = true;
                };
                panel.Controls.Add(lap);
                ++index;
            }
        }

        private void timerFordit_Tick(object sender, EventArgs e)
        {
            #region statisztika
            if (Lap.AktualisLap.sorszam == Lap.ElozoLap.sorszam)
            {
                if (!Lap.AktualisLap.ismerve && !Lap.ElozoLap.ismerve)
                {
                    ++vakTalalalat;
                }
            }
            else
            {
                if (Lap.AktualisLap.ismerve)
                {
                    ++elfelejtettLapok;
                }
                if (Lap.ElozoLap.ismerve)
                {
                    ++elfelejtettLapok;
                }
            }
            #endregion

            //if (Lap.AktualisLap.sorszam == Lap.ElozoLap.sorszam)
            if (Lap.AktualisLap.Image == Lap.ElozoLap.Image)
            {
                Lap.ElozoLap.levesz();
                LapLevesz(this, new LapLeveszEventArgs(int.Parse(Lap.ElozoLap.Name), int.Parse(Lap.ElozoLap.sorszam + "")));
                Lap.AktualisLap.levesz();
                LapLevesz(this, new LapLeveszEventArgs(int.Parse(Lap.AktualisLap.Name), int.Parse(Lap.AktualisLap.sorszam + "")));

                ++LevettParokSzama;
                if (LevettParokSzama == lapokSzama / 2)
                {
                    timer.Enabled = false;
                    timer.Stop();
                    JatekVegeEsemeny(this, new GameEventArgs(true));
                }
            }
            else
            {
                Lap.ElozoLap.megfordit();
                Lap.AktualisLap.megfordit();
            }
            Lap.AktualisLap.ismerve = true;
            Lap.ElozoLap.ismerve = true;
            Lap.MegforditottLapokSzama = 0;
            timerFordit.Enabled = false;
            Korvege(this, EventArgs.Empty);
        }
        private void sorszamSorsol()
        {
            sorszamok.Clear();
            while (sorszamok.Count < lapokSzama)
            {
                int kovSorszam = rnd.Next(1, ResourcePack.AktualisTema.KepekSzama);
                if (!sorszamok.Contains(kovSorszam))
                {
                    sorszamok.Add(kovSorszam);
                    sorszamok.Add(kovSorszam);
                }
            }
        }
        private void sorszamKever()
        {
            for (int i = 0; i < lapokSzama * 2; i++)
            {
                int seged = (rnd.Next(0, lapokSzama));
                sorszamok.Add(sorszamok[seged]);
                sorszamok.RemoveAt(seged);
            }
        }

        public void ResizeLaps()
        {
            int sgd = Width / sorokSzama;
            foreach (var item in panel.Controls.OfType<Lap>())
            {
                item.Size = new Size(sgd, sgd);
            }
        }

        public void visszahelyez()
        {
            foreach (var item in panel.Controls.OfType<Lap>())
            {
                item.visszarak();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now - kezdoIdo;
            string s = "";
            if (timeSpan.Days > 0)
            {
                s += s.Length > 0 ? ":" : "";
                s += timeSpan.Days;
            }
            if (timeSpan.Hours > 0)
            {
                s += s.Length > 0 ? ":" : "";
                s += timeSpan.Hours;
            }
            if (timeSpan.Minutes >= 0)
            {
                s += s.Length > 0 ? ":" : "";
                s += timeSpan.Minutes + " p";
            }
            if (timeSpan.Seconds >= 0)
            {
                s += s.Length > 0 ? ":" : "";
                s += timeSpan.Seconds + " mp";
            }
            //if (timeSpan.Milliseconds >= 0)
            //{
            //    s += s.Length > 0 ? ":" : "";
            //    s += timeSpan.Milliseconds;
            //}
            FrissitIdo(timeSpan, EventArgs.Empty);
        }

        public void LapValaszt(int index)
        {
            var s = panel.Controls.OfType<Lap>().Where(a => a.Name == index.ToString()).FirstOrDefault();
            if (s != null)
            {
                s.Felfordit();
            }

        }
    }
}
