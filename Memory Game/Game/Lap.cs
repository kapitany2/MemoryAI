using System;
using System.Drawing;
using System.Windows.Forms;

namespace Memory_Game.Game
{
    /// <summary>
    /// Kétoldalú kártyalap.
    /// </summary>
    class Lap : PictureBox
    {
        /// <summary>
        /// Aktuálisan felfordított lap.
        /// </summary>
        public static Lap AktualisLap;
        /// <summary>
        /// Előzőleg felfordított lap.
        /// </summary>
        public static Lap ElozoLap;
        /// <summary>
        /// Felfordított lapokat számolja
        /// </summary>
        public static byte MegforditottLapokSzama;

        public event EventHandler MasodikLapFelforditva;
        public event EventHandler LapKattintas;

        public int sorszam;
        public bool ismerve;
        private Image elulsoLap;
        private bool megforditva;

        /// <summary>
        /// Négyzet alapú kártyalap.
        /// </summary>
        /// <param name="sorszam">Kártyalap sorszáma</param>
        /// <param name="meret">Kártyalap mérete</param>
        public Lap(int sorszam, int meret)
        {
            this.sorszam = sorszam;
            this.BackColor = Color.Transparent;
            this.Width = this.Height = meret;
            this.megforditva = false;
            this.Image = ResourcePack.AktualisTema.Hatlap;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Margin = new Padding(0, 0, 0, 0);
            this.elulsoLap = ResourcePack.AktualisTema.images[sorszam];
            this.Click += Lap_Click;
            this.MouseEnter += Lap_MouseEnter;
            this.MouseLeave += Lap_MouseLeave;
        }

        private void Lap_MouseLeave(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Lap_MouseEnter(object sender, EventArgs e)
        {
            Kijelol();
        }

        private void Lap_Click(object sender, EventArgs e)
        {
            Felfordit();
        }

        public void Kijelol()
        {
            using (Graphics g = this.CreateGraphics())
            {
                Pen pen = new Pen(Color.FromArgb(130, Color.Yellow), Width / 4);
                g.DrawRectangle(pen, 0, 0, Width, Height);
            }
        }

        public void Felfordit()
        {
            if (this.Enabled && MegforditottLapokSzama < 2)
            {
                //if (!MasodikForditas)
                if (MegforditottLapokSzama == 0)
                {
                    ++MegforditottLapokSzama;
                    megfordit();
                    ElozoLap = AktualisLap = this;
                }
                //else if (MasodikForditas && !ElozoLap.Equals(this))
                else if (MegforditottLapokSzama == 1 && !ElozoLap.Equals(this))
                {
                    ++MegforditottLapokSzama;
                    megfordit();
                    AktualisLap = this;
                    MasodikLapFelforditva(this, EventArgs.Empty);
                }
                LapKattintas(this, EventArgs.Empty);
            }
        }

        public void megfordit()
        {
            megforditva = !megforditva;
            if (megforditva)
            {
                this.Image = elulsoLap;
            }
            else
            {
                this.Image = ResourcePack.AktualisTema.Hatlap;
            }
        }

        public void levesz()
        {
            this.Enabled = false;
            this.Image = ResourcePack.AktualisTema.LevettLap;
        }

        public void visszarak()
        {
            this.Image = elulsoLap;
        }
    }
}
