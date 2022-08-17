using Memory_Game.AI;
using Memory_Game.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game
{
    public partial class MainFrmAI : Form
    {
        AIControl AIControl = new AIControl();
        AISettingsForm aISettingsForm = new AISettingsForm();
        public MainFrmAI()
        {
            InitializeComponent();
            panelSgd.Controls.Add(AIControl);
        }

        private void MainFrmAI_Load(object sender, EventArgs e)
        {
            aISettingsForm.Visibility += (send, args) =>
            {

            };
            aISettingsForm.Debug += (send, args) =>
            {

            };
            aISettingsForm.TimerIntervalChanged += (send, args) =>
            {
                AIControl.timer1.Interval = (send as NumericUpDown).Value != 0 ? (int)(send as NumericUpDown).Value : 1;
            };
            aISettingsForm.Location = this.Location;
            int pnlsgdSmall = panelSgd.Width < panelSgd.Height ? panelSgd.Width : panelSgd.Height;
            AIControl.Size = new Size(pnlsgdSmall, pnlsgdSmall);
            AIControl.Location = new Point((panelSgd.Width - pnlsgdSmall) / 2, 0);
        }

        /// <summary>
        /// Inicializálja az AIControlt.
        /// </summary>
        /// <param name="kezd">AI elinduljon-e</param>
        /// <param name="image">Kép amit feldolgoz.</param>
        /// <param name="gameControl">Gamecontrol ami vezérli.</param>
        public void InitAI(bool kezd, Image image, GameControl gameControl)
        {
            AIControl.gameControl = gameControl;
            AIControl.Init(kezd, image);
        }

        /// <summary>
        /// Átadja a képet az AIControlnak
        /// </summary>
        /// <param name="image"></param>
        public void SendImage(Image image)
        {
            AIControl.SendImage(image);
        }

        private void MainFrmAI_ResizeEnd(object sender, EventArgs e)
        {
            AIControl.ResizeLaps();
        }

        private void panelSgd_Resize(object sender, EventArgs e)
        {
            int pnlsgdSmall = panelSgd.Width < panelSgd.Height ? panelSgd.Width : panelSgd.Height;
            AIControl.Size = new Size(pnlsgdSmall, pnlsgdSmall);
            AIControl.Location = new Point((panelSgd.Width - pnlsgdSmall) / 2, 0);
        }

        private void aIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AIControl.PAUSE = !AIControl.PAUSE;
            //AIControl.Next();
            AIControl.timer1.Enabled = !AIControl.PAUSE;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aISettingsForm.Show();
        }

        private void MainFrmAI_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}