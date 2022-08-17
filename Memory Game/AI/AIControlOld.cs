using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game.AI
{
    public partial class AIControlOld : UserControl
    {
        Random rnd = new Random();
        int lapokSzama = 4;
        int width, height;
        ImageHash hatlap;
        List<Image> kapottKepek = new List<Image>();
        int[] hanyadikvaltozas;
        ImageHash[] lapok;
        ImageHash[] kapottKepekHash;
        int[] lapPar = new int[2];
        public GameControl gameControl { get; set; }
        FlowLayoutPanel panel = new FlowLayoutPanel();
        Allapot allapot;
        int megtalaltakSzama;
        int elsovalasztott;

        public AIControlOld()
        {
            InitializeComponent();
            panel.Dock = DockStyle.Fill;
            this.Controls.Add(panel);
        }

        public void Init(int lapokszama, Image kezdokep)
        {
            GC.Collect();
            lapPar[0] = lapPar[1] = elsovalasztott = -1;
            megtalaltakSzama = 0;
            lapokSzama = lapokszama;
            kapottKepekHash = new ImageHash[lapokszama];
            ImageSplitList(kezdokep);
            hatlap = new ImageHash(kapottKepek[0]);
            Console.WriteLine("Hátlap:\n" + hatlap.ToString());
            hanyadikvaltozas = new int[lapokSzama];
            lapok = new ImageHash[lapokszama];
            panel.Controls.Clear();
            for (int i = 0; i < kapottKepek.Count; i++)
            {
                hanyadikvaltozas[i] = 0;
                lapok[i] = hatlap;
                panel.Controls.Add(new PictureBox()
                {
                    Enabled = true,
                    Name = i + "",
                    Image = kapottKepek[i],
                    Margin = new Padding(0, 0, 0, 0),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(panel.Width / (int)Math.Sqrt(lapokszama), panel.Height / (int)Math.Sqrt(lapokszama))
                });
            }
            CheckWhatsHappened();
            //timer.Enabled = true;
        }

        private void ImageSplitList(Image kezdokep)
        {
            width = kezdokep.Width / (int)Math.Sqrt(lapokSzama);
            height = kezdokep.Height / (int)Math.Sqrt(lapokSzama);
            var imgList = new Image[lapokSzama];
            var img = (Image)kezdokep.Clone();
            for (int i = 0; i < Math.Sqrt(lapokSzama); i++)
            {
                for (int j = 0; j < Math.Sqrt(lapokSzama); j++)
                {
                    var index = j * ((int)Math.Sqrt(lapokSzama)) + i;
                    imgList[index] = new Bitmap(width, height);
                    var graphics = Graphics.FromImage(imgList[index]);
                    graphics.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(i * width, j * height, width, height), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    var ss = new ImageHash(imgList[index] as Bitmap);
                    Console.WriteLine(index + ":\n" + ss.ToString());
                    kapottKepekHash[index] = new ImageHash(imgList[index]);
                }
            }
            kapottKepek = imgList.ToList();
        }

        public void SendImage(Image image)
        {
            //Console.WriteLine("\n\n");
            //Console.WriteLine("Hátlap:\n" + hatlap.ToString());
            //unsafe
            {
                //kép felosztás
                ImageSplitList(image);
                //változások mentése
                for (int i = 0; i < kapottKepek.Count; i++)
                {
                    //Console.Write(i + " ");
                    //if (!ImageHash.IsSameHash(kapottKepekHash[i], hatlap.Hash))
                    if (!hatlap.IsSameHash(kapottKepekHash[i]))
                    //if (ImageHash.ConvertToHash(kapottKepek[i] as Bitmap) != hatlap.Hash)
                    {
                        if (hanyadikvaltozas[i] <= 2)
                        {
                            var sgd = panel.Controls.OfType<PictureBox>().Where(a => a.Name == i + "").FirstOrDefault();
                            if (sgd != null)
                            {
                                if (!ImageHash.IsSameImage((sgd.Image as Bitmap), (kapottKepek[i] as Bitmap)))
                                {
                                    if (hanyadikvaltozas[i] == 0)
                                    {
                                        sgd.Image = kapottKepek[i];
                                        lapok[i] = new ImageHash(sgd.Image);
                                        hanyadikvaltozas[i] += 1;
                                        //Console.WriteLine(i + ". index változott, változások száma: " + hanyadikvaltozas[i]);
                                    }
                                    else if (hanyadikvaltozas[i] == 1)
                                    {
                                        hanyadikvaltozas[i] += 1;
                                        Panel x = new Panel();
                                        x.Dock = DockStyle.Fill;
                                        x.BackColor = Color.Transparent;
                                        x.Paint += (s, a) =>
                                        {
                                            Graphics g;
                                            g = a.Graphics;
                                            a.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(170, Color.Black)), 0, 0, sgd.Width, sgd.Height);

                                            Pen myPen = new Pen(Color.FromArgb(130, Color.Red));
                                            myPen.Width = 80 / int.Parse(Math.Sqrt(lapokSzama) + "");

                                            a.Graphics.DrawLine(myPen, 0, 0, sgd.Width, sgd.Height);
                                            a.Graphics.DrawLine(myPen, sgd.Width, 0, 0, sgd.Height);
                                        };
                                        sgd.Controls.Add(x);
                                        ++megtalaltakSzama;
                                    }
                                }
                            }
                        }
                    }
                }

                //vizsgálás mi történt
                CheckWhatsHappened();
            }
            GC.Collect();
        }
        private void CheckWhatsHappened()
        {
            //var s = kapottKepekHash.Where(a => !hatlap.IsSameHash(a)).ToList();
            ////var sgd = kapottKepek.GroupBy(x => x).SelectMany(g => g.Skip(1)).ToList();
            //Console.WriteLine(s.Count +"db nem hátlap");
            //Console.WriteLine();
            if (megtalaltakSzama < lapokSzama)
            {
                int hatlapSzamlalo = 0;
                for (int i = 0; i < kapottKepekHash.Length; i++)
                {
                    if (hatlap.IsSameHash(kapottKepekHash[i]))
                    {
                        ++hatlapSzamlalo;
                    }
                }

                if (hatlapSzamlalo == kapottKepekHash.Length - megtalaltakSzama)
                {
                    //Console.WriteLine("Első választás beállítva");
                    allapot = Allapot.ElsoValasztas;
                }
                else if (hatlapSzamlalo == kapottKepekHash.Length - 1 - megtalaltakSzama)
                {
                    //Console.WriteLine("Második választás beállítva");
                    allapot = Allapot.MasodikValasztas;
                }
                else if (hatlapSzamlalo == kapottKepekHash.Length - 2 - megtalaltakSzama)
                {
                    //Console.WriteLine("Várakozás beállítva");
                    allapot = Allapot.Varakozik;
                }
            }
            else
            {
                //Console.WriteLine("Vége beállítva");
                allapot = Allapot.Vege;
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {

        }

        public void Next()
        {
            Console.WriteLine("\n\n\n");
            CheckWhatsHappened();
            switch (allapot)
            {
                case Allapot.ElsoValasztas:
                    Console.WriteLine("Ki kell választani az elsőt");
                    LapValasztas();
                    break;
                case Allapot.MasodikValasztas:
                    Console.WriteLine("Ki kell választani a másodikat");
                    LapValasztas();
                    break;
                case Allapot.Varakozik:
                    Console.WriteLine("Várni kell hogy felforduljon");
                    break;
                case Allapot.Levesz:
                    Console.WriteLine("Levesz");
                    break;
                case Allapot.Visszafordit:
                    Console.WriteLine("Visszafordit");
                    break;
                case Allapot.Ellenoriz:
                    Console.WriteLine("Ellenőrzés");
                    break;
                case Allapot.Vege:
                    Console.WriteLine("Vége");
                    timer.Enabled = false;
                    timer.Stop();
                    break;
                default:
                    break;
            }
            allapot = Allapot.Varakozik;
        }

        private void LapValasztas()
        {
            Console.WriteLine("########### Lap választás ###########");
            if (allapot == Allapot.ElsoValasztas)
            {
                Console.WriteLine("Első lap választás");
                if (egyezoVanE())
                {
                    Console.Write("Van egyező, ezt választom: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(lapPar[0] + "");
                    Console.ForegroundColor = ConsoleColor.White;
                    elsovalasztott = lapPar[0];
                    gameControl.LapValaszt(lapPar[0]);
                }
                else
                {
                    Console.WriteLine("Nincs egyező: ");
                    Console.Write("Laphúzás: ");
                    lapPar[0] = ismeretlenLapIndex();
                    elsovalasztott = lapPar[0];
                    Console.WriteLine(lapPar[0] + "");
                    gameControl.LapValaszt(lapPar[0]);
                }
            }
            else if (allapot == Allapot.MasodikValasztas)
            {
                Console.WriteLine("Második lap választás");
                if (egyezoVanE())
                {
                    Console.Write("Van egyező, ezt választom: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    int masodikvalasztott = elsovalasztott == lapPar[1] ? lapPar[0] : lapPar[1];
                    Console.WriteLine(masodikvalasztott + "");
                    Console.ForegroundColor = ConsoleColor.White;
                    gameControl.LapValaszt(masodikvalasztott);
                }
                else
                {
                    Console.WriteLine("Nincs egyező ");
                    Console.Write("Laphúzás:");
                    lapPar[1] = ismeretlenLapIndex();
                    Console.WriteLine(lapPar[1] + "");
                    gameControl.LapValaszt(lapPar[1]);
                }
            }
        }

        private int ismeretlenLapIndex()
        {
            List<int> uresLista = new List<int>();
            for (int i = 0; i < kapottKepekHash.Length; i++)
            {
                if (hatlap.IsSameHash(kapottKepekHash[i]))
                {
                    uresLista.Add(i);
                }
            }
            return uresLista[rnd.Next(0, uresLista.Count)];
        }

        private bool egyezoVanE()
        {
            //var sgd = kapottKepekHash.Where(a => !hatlap.IsSameHash(a)).ToList().GroupBy(b => b.Hash).SelectMany(c => c.Skip(1)).ToList();
            //var sa = kapottKepekHash.Where(a => !hatlap.IsSameHash(a)).ToList();
            //var sb = sa.GroupBy(b => b.Hash);
            //var sc = sb.SelectMany(a => a.Skip(1)).ToList();
            //var sc = kapottKepekHash.GroupBy(a => a.Hash);
            var sc = from kkh in kapottKepekHash
                     where !kkh.IsSameHash(hatlap)
                     group kkh by kkh.HashString into g
                     select g;

            foreach (var item in sc)
            {
                Console.WriteLine(item.ToString());
                foreach (var it in item)
                {

                    Console.WriteLine(it);
                }
            }
            var sgd = kapottKepekHash.Where(a => !hatlap.IsSameHash(a)).ToList().GroupBy(b => b.Hash).SelectMany(c => c.Skip(1)).ToList();
            if (sgd.Count > 0)
            {
                parosLapSet(sgd[0]);
            }
            return sgd.Count > 0;
        }

        private void parosLapSet(ImageHash eztkeresem)
        {
            bool masodik = false;
            for (int i = 0; i < kapottKepekHash.Length; i++)
            {
                if (kapottKepekHash[i] == eztkeresem)
                {
                    lapPar[masodik ? 0 : 1] = i;
                    masodik = true;
                }
            }
        }

        /// <summary>
        /// Újraméretezi a képeket, hogy alkalmazkodjon az ablakhoz.
        /// </summary>
        public void ResizePBs()
        {
            int sgd = panel.Width / (int)Math.Sqrt(lapokSzama);
            foreach (var item in panel.Controls.OfType<PictureBox>())
            {
                item.Size = new Size(sgd, sgd);
            }
        }

        private void LapFordit(int index)
        {
            gameControl.LapValaszt(index);
        }

    }
}
