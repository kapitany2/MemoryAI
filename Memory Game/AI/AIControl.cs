using Memory_Game.Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game.AI
{
    public partial class AIControl : UserControl
    {
        #region Properties
        Allapot Allapot;
        Random rnd = new Random();
        FlowLayoutPanel panel = new FlowLayoutPanel();
        ImageHash hatlap { get; set; }
        List<CPanel> lapok { get; set; } = new List<CPanel>();
        List<Image> kapottKepek { get; set; } = new List<Image>();
        ImageHash[] kapottKepekHash { get; set; }
        List<int> tiltott { get; set; } = new List<int>();
        int[] lapParIndex { get; set; } = new int[2];
        int[] lapParIndexElozo { get; set; } = new int[2];
        int lapokSzama { get; set; } = 4;
        int megtalaltakSzama { get; set; }
        int ismeretlenLapokSzama { get; set; }
        int elsoValasztottIndexe { get; set; }
        bool sorbaHaladas { get; set; }
        internal bool PAUSE { get; set; } = false;
        bool egyezoLapok { get; set; }
        int eloszorEztAkaromLeszedni { get; set; }
        int egyezoLapokSegedIndex { get; set; }
        int elozolegMegtalaltakSzama { get; set; }

        public GameControl gameControl { get; set; }
        #endregion
        public AIControl()
        {
            InitializeComponent();
            panel.Dock = DockStyle.Fill;
            this.Controls.Add(panel);
        }

        /// <summary>
        /// Inicializálja az AI-t.
        /// </summary>
        /// <param name="kezd">Elkezdődjön-e az AI</param>
        /// <param name="kezdokep">Kép, amit feldolgoz az AI.</param>
        public void Init(bool kezd, Image kezdokep)
        {
            DConsole.WriteLine("########## Inicializálás ##########");
            GC.Collect();
            tiltott.Clear();
            egyezoLapokSegedIndex = 1;
            eloszorEztAkaromLeszedni = -1;
            egyezoLapok = false;
            lapParIndexElozo[0] = lapParIndexElozo[1] = lapParIndex[0] = lapParIndex[1] = elsoValasztottIndexe = -1;
            megtalaltakSzama = 0;
            lapokSzama = LapokSzamaBeallit(kezdokep);
            ismeretlenLapokSzama = lapokSzama;
            //lapokSzama = lapokszama;
            kapottKepekHash = new ImageHash[lapokSzama];
            kapottKepek = ImageSplit(kezdokep);
            hatlap = new ImageHash(kapottKepek[0]);
            lapok = new List<CPanel>();
            panel.Controls.Clear();
            for (int i = 0; i < kapottKepek.Count; i++)
            {
                CPanel cPanel = new CPanel()
                {
                    //Enabled = true
                    Name = i.ToString(),
                    Margin = new Padding(0, 0, 0, 0),
                    ImageHash = new ImageHash(kapottKepek[i]),
                    Size = new Size(panel.Width / (int)Math.Sqrt(lapokSzama), panel.Height / (int)Math.Sqrt(lapokSzama))

                };
                cPanel.PBox.Image = kapottKepek[i];
                lapok.Add(cPanel);
                panel.Controls.Add(cPanel);
            }
            CheckWhatsHappened();
            GC.Collect();
            PAUSE = !kezd;
            timer1.Start();
        }


        /// <summary>
        /// Kiválaszt egy lapot.
        /// </summary>
        private void LapValasztas()
        {
            var haha = lapok.Where(a => !a.tiltott && a.hanyadikValtozas != 2).ToList().Select(b => b.Name).ToList();
            if (ismeretlenLapokSzama == 0 && lapok.Count - megtalaltakSzama > 0 && haha.Count() == 0)
            {
                DConsole.WriteLine("Egyezők maradtak.");
                var maradek = lapok.Where(a => a.hanyadikValtozas == 1).ToList();
                tiltott.Clear();
                foreach (var item in maradek)
                {
                    tiltott.Add(int.Parse(item.Name));
                }
                egyezoLapok = true;
                tiltott.Sort();
                eloszorEztAkaromLeszedni = tiltott[0];
            }
            if (egyezoLapok)
            {
                LapValasztasEgyezoLapokSzerint();
                return;
            }
            DConsole.Write("######## Új választás ########\nLap választás: ");
            if (Allapot == Allapot.ElsoValasztas)
            {
                DConsole.WriteLine("ELSŐ");
                if (egyezoVanE())
                {
                    DConsole.Write("Van egyező: ");
                    elsoValasztottIndexe = lapParIndex[0];
                    DConsole.WriteLine(elsoValasztottIndexe + "", ConsoleColor.Cyan);
                    gameControl.LapValaszt(lapParIndex[0]);

                }
                else
                {
                    DConsole.Write("Nincs egyező. Ismeretlen lap húzás: ");
                    elsoValasztottIndexe = lapParIndex[0] = ismeretlenLapIndex();
                    DConsole.WriteLine(elsoValasztottIndexe + "", ConsoleColor.Cyan);
                    gameControl.LapValaszt(lapParIndex[0]);
                }
            }
            else if (Allapot == Allapot.MasodikValasztas)
            {
                DConsole.WriteLine("MÁSODIK");
                if (egyezoVanE())
                {
                    DConsole.Write("Van egyező: ");
                    int masodikvalasztott = elsoValasztottIndexe == lapParIndex[1] ? lapParIndex[0] : lapParIndex[1];
                    DConsole.WriteLine(masodikvalasztott + "", ConsoleColor.Cyan);
                    gameControl.LapValaszt(masodikvalasztott);
                }
                else
                {
                    DConsole.Write("Nincs egyező. Ismeretlen lap húzás: ");
                    lapParIndex[1] = ismeretlenLapIndex();
                    DConsole.WriteLine(lapParIndex[1] + "", ConsoleColor.Cyan);
                    gameControl.LapValaszt(lapParIndex[1]);
                }
                if (Enumerable.SequenceEqual(lapParIndex, lapParIndexElozo))
                {
                    if (!tiltott.Contains(lapParIndex[0]) && lapParIndexElozo.Contains(lapParIndex[0]))
                    {
                        Console.WriteLine("tiltólistára: " + lapParIndex[0]);
                        lapok.Where(a => a.Name == lapParIndex[0] + "").FirstOrDefault().tiltott = true;
                        tiltott.Add(lapParIndex[0]);
                    }
                    if (!tiltott.Contains(lapParIndex[1]) && lapParIndexElozo.Contains(lapParIndex[1]))
                    {
                        Console.WriteLine("tiltólistára: " + lapParIndex[1]);
                        lapok.Where(a => a.Name == lapParIndex[1] + "").FirstOrDefault().tiltott = true;
                        tiltott.Add(lapParIndex[1]);
                    }

                }
                lapParIndexElozo[0] = lapParIndex[0];
                lapParIndexElozo[1] = lapParIndex[1];
            }
        }

        /// <summary>
        /// Beállítja a lappárt, egyező lapok szerinti módban.
        /// </summary>
        private void LapValasztasEgyezoLapokSzerint()
        {
            if (elozolegMegtalaltakSzama < megtalaltakSzama)
            {
                tiltott.Remove(lapParIndex[0]);
                tiltott.Remove(lapParIndex[1]);
                eloszorEztAkaromLeszedni = -1;
                egyezoLapokSegedIndex = 1;
                elozolegMegtalaltakSzama = megtalaltakSzama;
            }


            if (eloszorEztAkaromLeszedni == -1)
            {
                eloszorEztAkaromLeszedni = tiltott[0];
                egyezoLapokSegedIndex = 1;
            }

            if (Allapot == Allapot.ElsoValasztas)
            {
                gameControl.LapValaszt(eloszorEztAkaromLeszedni);
            }
            else if (Allapot == Allapot.MasodikValasztas)
            {
                elozolegMegtalaltakSzama = megtalaltakSzama;
                lapParIndex[0] = eloszorEztAkaromLeszedni;
                lapParIndex[1] = tiltott[egyezoLapokSegedIndex];
                gameControl.LapValaszt(tiltott[egyezoLapokSegedIndex]);
                egyezoLapokSegedIndex++;
            }
        }

        /// <summary>
        /// Visszaad egy indexet amit még nem ismerünk.
        /// </summary>
        /// <returns></returns>
        private int ismeretlenLapIndex()
        {
            List<int> uresLista = new List<int>();
            for (int i = 0; i < lapok.Count; i++)
            {
                //if (hatlap.IsSameHash(kapottKepekHash[i]))
                //    uresLista.Add(i);
                if (lapok[i].hanyadikValtozas == 0)
                    uresLista.Add(i);
            }
            //hibakezelés
            if (uresLista.Count == 0)
                for (int i = 0; i < lapok.Count; i++)
                    if (lapok[i].hanyadikValtozas == 1)
                        uresLista.Add(i);

            if (sorbaHaladas)
                return uresLista[0];
            else
                return uresLista[rnd.Next(0, uresLista.Count)];
        }

        /// <summary>
        /// Megnézi, hogy van-e egyező kép.
        /// </summary>
        /// <returns></returns>
        private bool egyezoVanE()
        {
            //DConsole.WriteLine("\n");
            //var xx = lapok.Where(a => a.hanyadikValtozas == 1).ToList();
            //foreach (var item in xx)
            //{
            //    Console.WriteLine(item.ImageHash.getHash);
            //}

            //var sgd = kapottKepekHash.Where(a => !hatlap.IsSameHash(a)).ToList().GroupBy(b => b.getHash).SelectMany(c => c.Skip(1)).ToList();
            var aa = lapok.Where(a => !a.tiltott && a.hanyadikValtozas == 1).ToList();
            var bb = aa.GroupBy(b => b.ImageHash.HashString);
            Console.WriteLine("Csoportok:");
            foreach (var item in bb)
            {
                Console.WriteLine("Csoport:");
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.Name + "\t" + item2.ImageHash);
                }
            }
            Console.WriteLine("Select");
            var cc = bb.SelectMany(c => c.Skip(1)).ToList();
            foreach (var item in cc)
            {
                Console.WriteLine(item.Name + "\t" + item.ImageHash);
            }
            var sgd = cc;
            int i = 0;
            if (sgd.Count > 0)
            {
                if (tiltott.Contains(int.Parse(sgd[i].Name)))
                {
                    return false;
                }
                lapParIndex = setLapPar(sgd[i].ImageHash);
                //DConsole.WriteLine(sgd.Count + "darab egyező");
                //do
                //{
                //    lapParIndexElozo = lapParIndex;
                //    lapParIndex = setLapPar(sgd[i].ImageHash, i);
                //    if (SameLapPar(lapParIndexElozo, lapParIndex))
                //    {
                //        DConsole.WriteLine("Ugyan az a lépés", ConsoleColor.Red);
                //    }
                //    ++i;
                //} while (SameLapPar(lapParIndexElozo, lapParIndex) && i < sgd.Count);

            }
            //if (i >= sgd.Count - 1)
            //{
            //    return false;
            //}

            return sgd.Count > 0;
        }

        /// <summary>
        /// Megkeresi a kapott képnek a lehetséges párját.
        /// </summary>
        /// <param name="imageHash">Ennek a képnek keresi meg.</param>
        /// <returns></returns>
        private int[] setLapPar(ImageHash imageHash)
        {
            bool masodik = false;
            int[] visszaad = new int[2];
            for (int i = 0; i < lapok.Count; i++)
                if (lapok[i].ImageHash.IsSameHash(imageHash) && lapok[i].tiltott == false && lapok[i].hanyadikValtozas == 1)
                {
                    visszaad[masodik ? 0 : 1] = i;
                    masodik = true;
                }
            return visszaad;
        }

        private void setElozoLapPar(ImageHash imageHash)
        {
            bool masodik = false;
            for (int i = 0; i < lapok.Count; i++)
                if (lapok[i].ImageHash.IsSameHash(imageHash))
                {
                    lapParIndex[masodik ? 0 : 1] = i;
                    masodik = true;
                }
        }

        private bool SameLapPar(int[] egyik, int[] masik)
        {
            if (egyik.Length == masik.Length && masik.Length == 2)
                if ((egyik[0] == masik[0] || egyik[0] == masik[1]) && (egyik[1] == masik[0] || egyik[1] == masik[1]))
                    return true;
            return false;
        }


        /// <summary>
        /// Megnézi milyen állapot van jelenleg.
        /// </summary>
        private void CheckWhatsHappened()
        {
            DConsole.Write("Állapot beállítás erre: ");
            if (megtalaltakSzama < lapokSzama)
            {
                int hatlapSzamlalo = 0;
                for (int i = 0; i < kapottKepekHash.Length; i++)

                    if (hatlap.IsSameHash(kapottKepekHash[i]))

                        ++hatlapSzamlalo;

                if (hatlapSzamlalo == kapottKepekHash.Length - megtalaltakSzama)
                {
                    DConsole.WriteLine("Első választás");
                    Allapot = Allapot.ElsoValasztas;
                }
                else if (hatlapSzamlalo == kapottKepekHash.Length - megtalaltakSzama - 1)
                {
                    DConsole.WriteLine("Második választás");
                    Allapot = Allapot.MasodikValasztas;
                }
                else if (hatlapSzamlalo == kapottKepekHash.Length - megtalaltakSzama - 2)
                {
                    DConsole.WriteLine("Várakozás");
                    Allapot = Allapot.Varakozik;
                }
            }
            else
            {
                DConsole.WriteLine("Vége");
                Allapot = Allapot.Vege;
            }
        }

        /// <summary>
        /// Felosztja a képet sok kis darabra.
        /// </summary>
        /// <param name="kezdokep">Ezt a képet osztja fel</param>
        private List<Image> ImageSplit(Image kezdokep)
        {
            int width = kezdokep.Width / (int)Math.Sqrt(lapokSzama);
            int height = kezdokep.Height / (int)Math.Sqrt(lapokSzama);
            var imgList = new Image[lapokSzama];
            var img = (Image)kezdokep.Clone();

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            for (int i = 0; i < Math.Sqrt(lapokSzama); i++)
            {
                for (int j = 0; j < Math.Sqrt(lapokSzama); j++)
                {
                    var index = j * ((int)Math.Sqrt(lapokSzama)) + i;
                    imgList[index] = new Bitmap(width, height);
                    var graphics = Graphics.FromImage(imgList[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(i * width, j * height, width, height), GraphicsUnit.Pixel);
                    graphics.Dispose();

                    kapottKepekHash[index] = new ImageHash(imgList[index]);
                    //Console.WriteLine(String.Format("{0,3}", index) + ": " + kapottKepekHash[index]);
                }
            }
            //stopwatch.Stop();
            //TimeSpan ts = stopwatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //ts.Hours, ts.Minutes, ts.Seconds,
            //ts.Milliseconds / 10);
            //Console.WriteLine("ImageSplit RunTime " + elapsedTime);
            return imgList.ToList();
        }

        /// <summary>
        /// Beállítja hány lapot lehet felfordítani.
        /// </summary>
        /// <param name="kezdokep">Kép amit megvizsgál, hogy hány egyforma kis képet lehet csinálni belőle.</param>
        /// <returns></returns>
        private int LapokSzamaBeallit(Image kezdokep)
        {
            int visszaad = 1;
            var sgd = FeloszthatoE(kezdokep, 1);
            if (sgd != 1)
            {
                visszaad = sgd;
            }
            DConsole.WriteLine("Sor/oszlop: " + visszaad + "\nLapok száma: " + visszaad * visszaad);
            return visszaad * visszaad;
        }

        /// <summary>
        /// Megvizsgálja, hogy egy képet fel tud úgy osztani egyenlő részre, hogy ugyan az szerepeljen rajta.
        /// </summary>
        /// <param name="image">Kép</param>
        /// <param name="szorzat">DB egyforma kép</param>
        /// <returns></returns>
        private int FeloszthatoE(Image image, int szorzat)
        {
            int[] prim = { 2, 3, 5 };
            for (int x = 0; x < prim.Length; x++)
            {
                int tesztLapokSzama = prim[x] * prim[x];
                int sqrtLapSzam = (int)Math.Sqrt(tesztLapokSzama);
                int width = image.Width / sqrtLapSzam;
                int height = image.Height / sqrtLapSzam;
                var imgList = new Image[tesztLapokSzama];
                var img = (Image)image.Clone();
                var teszt = new ImageHash[tesztLapokSzama];
                for (int i = 0; i < sqrtLapSzam; i++)
                {
                    for (int j = 0; j < sqrtLapSzam; j++)
                    {
                        var index = j * ((int)Math.Sqrt(tesztLapokSzama)) + i;
                        imgList[index] = new Bitmap(width, height);
                        var graphics = Graphics.FromImage(imgList[index]);
                        graphics.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(i * width, j * height, width, height), GraphicsUnit.Pixel);
                        graphics.Dispose();
                        //new Debug.ShowImage(imgList[index], index, prim[x]).Show();
                        teszt[index] = new ImageHash(imgList[index]);

                    }
                }
                bool egyezo = true;
                for (int i = 0; i < teszt.Length - 1; i++)
                {
                    if (teszt[i] == null)
                    {
                        egyezo = false;
                        break;
                    }
                    if (!teszt[i].IsSameHash(teszt[i + 1]))
                    {
                        egyezo = false;
                    }
                }
                if (egyezo)
                {
                    //new Debug.ShowImage(imgList[0], x, prim[x]).Show();
                    return FeloszthatoE(imgList[0], prim[x] * szorzat);
                }
            }
            return szorzat;
        }

        /// <summary>
        /// Egy kapott képet feldolgol, új lapokat felhelyezi a látott panelre, ha talált egyezőt, X-et fest rá.
        /// </summary>
        /// <param name="image"></param>
        public void SendImage(Image image)
        {
            kapottKepek = ImageSplit(image);
            for (int i = 0; i < kapottKepek.Count; i++)
            {
                if (!hatlap.IsSameHash(kapottKepekHash[i]))
                {
                    if (lapok[i].hanyadikValtozas <= 2)
                    {
                        if (!ImageHash.IsSameHash(lapok[i].ImageHash.Hash, kapottKepekHash[i].Hash))
                        {
                            if (lapok[i].hanyadikValtozas == 0)
                            {
                                lapok[i].PBox.Image = kapottKepek[i];
                                lapok[i].ImageHash = new ImageHash(kapottKepek[i]);
                                ++lapok[i].hanyadikValtozas;
                                --ismeretlenLapokSzama;
                            }
                            else if (lapok[i].hanyadikValtozas == 1)
                            {
                                lapok[i].hanyadikValtozas += 1;
                                int w = lapok[i].PBox.Width;
                                int h = lapok[i].PBox.Height;
                                Panel x = new Panel();
                                x.Dock = DockStyle.Fill;
                                x.BackColor = Color.Transparent;
                                x.Paint += (send, args) =>
                                {
                                    Graphics g;
                                    g = args.Graphics;
                                    args.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(170, Color.Black)), 0, 0, w, h);

                                    Pen myPen = new Pen(Color.FromArgb(130, Color.Red));
                                    myPen.Width = 80 / int.Parse(Math.Sqrt(lapokSzama) + "");

                                    args.Graphics.DrawLine(myPen, 0, 0, w, h);
                                    args.Graphics.DrawLine(myPen, w, 0, 0, h);
                                };
                                lapok[i].PBox.Controls.Add(x);
                                ++megtalaltakSzama;
                            }
                        }
                    }
                }
            }
            //CheckWhatsHappened();
            GC.Collect();
            //new Thread(Next).Start();
            //Next();
        }
        /// <summary>
        /// Újraméretezi a képeket, hogy alkalmazkodjon az ablakhoz.
        /// </summary>
        public void ResizeLaps()
        {
            int sgd = panel.Width / (int)Math.Sqrt(lapokSzama);
            foreach (var item in panel.Controls.OfType<CPanel>())
            {
                item.Size = new Size(sgd, sgd);
            }
        }

        /// <summary>
        /// Következő folyamatot elvégzi.
        /// </summary>
        public void Next()
        {
            if (PAUSE)
                return;
            CheckWhatsHappened();
            DConsole.Write("\n\n\nÁllapot: ", ConsoleColor.Yellow);
            switch (Allapot)
            {
                case Allapot.ElsoValasztas:
                    DConsole.WriteLine("Ki kell választani az elsőt");
                    LapValasztas();
                    break;
                case Allapot.MasodikValasztas:
                    DConsole.WriteLine("Ki kell választani a másodikat");
                    LapValasztas();
                    break;
                case Allapot.Varakozik:
                    DConsole.WriteLine("Várni kell hogy felforduljon");
                    break;
                case Allapot.Levesz:
                    DConsole.WriteLine("Levesz");
                    break;
                case Allapot.Visszafordit:
                    DConsole.WriteLine("Visszafordit");
                    break;
                case Allapot.Ellenoriz:
                    DConsole.WriteLine("Ellenőrzés");
                    break;
                case Allapot.Vege:
                    DConsole.WriteLine("Vége");
                    timer1.Enabled = false;
                    timer1.Stop();
                    //timer.Enabled = false;
                    //timer.Stop();
                    break;
                default:
                    break;
            }
            Allapot = Allapot.Varakozik;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Next();
        }
    }
}