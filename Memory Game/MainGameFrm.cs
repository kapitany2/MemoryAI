using Memory_Game.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Memory_Game
{
    public partial class MainGameFrm : Form
    {
        #region Console
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        #endregion
        internal GameControl gameControl;
        private SettingsControl settingsControl;
        private MainFrmAI MainFrmAI;
        public MainGameFrm()
        {
            gameControl = new GameControl();
            MainFrmAI = new MainFrmAI();
            MainFrmAI.Show();
            settingsControl = new SettingsControl();
            InitializeComponent();
            //toolStripStatusLabelFake.Text = "";
        }

        private void MainGameFrm_Load(object sender, EventArgs e)
        {
            Console.SetWindowSize(300, Console.WindowHeight);
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            settingsControl.Saved += (send, args) =>
            {
                switch (settingsControl.trackBarForditSebesseg.Value)
                {
                    case 1:
                        gameControl.timerFordit.Interval = 100;
                        break;
                    case 2:
                        gameControl.timerFordit.Interval = 500;
                        break;
                    case 3:
                        gameControl.timerFordit.Interval = 1000;
                        break;
                    case 4:
                        gameControl.timerFordit.Interval = 1500;
                        break;
                    case 5:
                        gameControl.timerFordit.Interval = 2000;
                        break;
                    default:
                        break;
                }
                gameControl.sorokSzama = (int)settingsControl.numericUpDown_SorokSzama.Value;
                gameControl.lapokSzama = (int)Math.Pow((int)settingsControl.numericUpDown_SorokSzama.Value, 2);

                gameControl.Enabled = true;

                gameControl.Init();
                using (Bitmap b = new Bitmap(gameControl.Width, gameControl.Height))
                {
                    gameControl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
                    MainFrmAI.InitAI(settingsControl.checkBox_StartAI.Checked, b, gameControl);
                }
            };
            settingsControl.Showed += (send, args) =>
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                statusStrip.SizingGrip = true;
                gameControl.Enabled = false;
            };
            settingsControl.Hided += (send, args) =>
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                statusStrip.SizingGrip = false;
                gameControl.Enabled = true;
            };
            settingsControl.ChangedRowColumn += (send, args) =>
            {
                //if (gameControl.Width % (int)(send as NumericUpDown).Value == 0)
                //{
                //    settingsControl.Message("");
                //}
                //else
                //{
                //    settingsControl.Message("Megfelelő ablakméret beállítva.");
                //    while (gameControl.Width % (int)(send as NumericUpDown).Value != 0)
                //    {
                //        this.Width += 1;
                //        this.Height += 1;
                //        this.CenterToScreen();
                //    }
                //}
            };

            panelSgd.Controls.Add(gameControl);

            gameControl.JatekVegeEsemeny += (send, args) =>
            {
                using (Bitmap b = new Bitmap(gameControl.Width, gameControl.Height))
                {
                    gameControl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
                    MainFrmAI.SendImage(b);
                }
                gameControl.visszahelyez();
                settingsControl.Show();
            };
            gameControl.LapForditottEsemeny += (send, args) =>
            {
                //
                //Console.WriteLine("katt");
                using (Bitmap b = new Bitmap(gameControl.Width, gameControl.Height))
                {
                    gameControl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
                    MainFrmAI.SendImage(b);
                }
            };
            gameControl.LapLevesz += (send, args) =>
            {
                //Console.WriteLine();
            };
            gameControl.Korvege += (send, args) =>
            {
                toolStripStatusLabel1.Text = String.Format("Levett párok: {0}  Vak találat: {1}  Elfelejtett lapok: {2}  Hátralévő párok: {3}", gameControl.LevettParokSzama, gameControl.vakTalalalat, gameControl.elfelejtettLapok, gameControl.lapokSzama / 2 - gameControl.LevettParokSzama);
                using (Bitmap b = new Bitmap(gameControl.Width, gameControl.Height))
                {
                    gameControl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
                    MainFrmAI.SendImage(b);
                }
            };
            gameControl.Elkezdodott += (send, args) =>
            {

            };
            gameControl.FrissitIdo += (send, args) =>
            {
                toolStripStatusLabel_Time.Text = "Eltelt idő: " + send;
            };
            //Lap.LapKattintas += (send, args) =>
            //{
            //    using (Bitmap b = new Bitmap(gameControl.Width, gameControl.Height))
            //    {
            //        gameControl.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
            //        MainFrmAI.SendImage(b);
            //    }
            //};
            ResourcePack.resourcePacks = new List<ResourcePack>();
            ResourcePacksLoad();


            panelSgd.Controls.Add(settingsControl);
            //settingsControl.Hide();
            settingsControl.BringToFront();

            gameControlResize();
            settingsControl.Location = new Point((panelSgd.Width - settingsControl.Width) / 2, (panelSgd.Height - settingsControl.Height) / 2);

        }

        private void ResourcePacksLoad()
        {
            try
            {
                var resourceDirectories = Directory.GetDirectories("ResourcePack").ToList();


                foreach (var resourceDirectory in resourceDirectories)
                {
                    try
                    {
                        var files = Directory.GetFiles(resourceDirectory).Where(
                        a =>
                        isNumber(a.Split('.')[0].Split('\\')[2])
                        ).ToList();

                        if (files.Count > 3)
                        {
                            if (Directory.GetFiles(resourceDirectory).Where(
                                a =>
                                a.Split('.')[0].Split('\\')[2].Equals("hatlap")
                                ).ToList().Count > 0
                                &&
                                Directory.GetFiles(resourceDirectory).Where(
                                a =>
                                a.Split('.')[0].Split('\\')[2].Equals("ures")
                                ).ToList().Count > 0)
                            {
                                List<Image> images = new List<Image>();
                                for (int i = 1; i <= files.Count; i++)
                                {
                                    images.Add(Image.FromFile(resourceDirectory + "\\" + i + ".png"));
                                }
                                ResourcePack.resourcePacks.Add(new ResourcePack()
                                {
                                    EleresiUt = resourceDirectory + "\\",
                                    KepekSzama = files.Count,
                                    Name = resourceDirectory.Split('\\')[1],
                                    images = images
                                }); ;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                foreach (var resourcePack in ResourcePack.resourcePacks)
                {
                    settingsControl.comboBox_Theme.Items.Add(resourcePack.Name);
                }
                settingsControl.comboBox_Theme.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Nem található forrásfájl.", "Hiba");
            }
        }

        private bool isNumber(string szam)
        {
            foreach (char betu in szam)
            {
                if (betu < 48 || betu > 57)
                {
                    return false;
                }
            }
            return true;
        }

        private void panelSgd_Resize(object sender, EventArgs e)
        {
            gameControlResize();
            settingsControl.Location = new Point((panelSgd.Width - settingsControl.Width) / 2, (panelSgd.Height - settingsControl.Height) / 2);
            //toolStripStatusLabel1.Text = panelSgd.Size.ToString() + "||" + settingsControl.Left;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsControl.Show();
        }

        private void MainGameFrm_ResizeEnd(object sender, EventArgs e)
        {
            gameControl.ResizeLaps();
        }

        private void gameControlResize()
        {
            int pnlsgdSmall = panelSgd.Width < panelSgd.Height ? panelSgd.Width : panelSgd.Height;
            int maxSize = legnagyobbMeret(pnlsgdSmall);
            gameControl.Size = new Size(maxSize, maxSize);
            gameControl.Location = new Point((panelSgd.Width - maxSize) / 2, (panelSgd.Height - maxSize) / 2);
        }

        private int legnagyobbMeret(int maxmeret)
        {
            //int[] szamok = { 2, 4, 6, 8, 10, 12 };
            List<int> szamok = new List<int>();
            for (int i = 2; i <= settingsControl.numericUpDown_SorokSzama.Maximum; i += 2)
            {
                szamok.Add(i);
            }
            int sgd = szamok[szamok.Count - 1];
            int visszaad = sgd;
            do
            {
                bool hozzaad = true;
                foreach (var item in szamok)
                {
                    if (sgd % item != 0)
                    {
                        hozzaad = false;
                        break;
                    }
                }
                if (hozzaad)
                {
                    visszaad = sgd;
                }

                sgd += szamok[szamok.Count - 1];
            } while (sgd <= maxmeret);
            return visszaad;
        }

        private void aIOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainFrmAI.Show();
        }
    }
}