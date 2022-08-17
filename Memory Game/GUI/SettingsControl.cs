using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Memory_Game.GUI
{
    public partial class SettingsControl : UserControl
    {
        public EventHandler Saved;
        public EventHandler Showed;
        public EventHandler Hided;
        public EventHandler ChangedRowColumn;
        Point PanelMouseDownLocation;
        public SettingsControl()
        {
            InitializeComponent();
        }

        public void Message(string message)
        {
            labelInfo.Text = message;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            Saved(this, EventArgs.Empty);
            this.Hide();
        }

        private void comboBox_Theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResourcePack.AktualisTema = ResourcePack.resourcePacks.Where(a => a.Name == comboBox_Theme.SelectedItem.ToString()).FirstOrDefault();

            int hanyszorhanyas = 2;
            while (ResourcePack.AktualisTema.KepekSzama >= hanyszorhanyas * hanyszorhanyas)
            {
                numericUpDown_SorokSzama.Maximum = hanyszorhanyas;
                hanyszorhanyas += 2;
            }
            ResourcePack.AktualisTema.Hatlap = Image.FromFile(ResourcePack.AktualisTema.EleresiUt + "hatlap" + ".png");
            ResourcePack.AktualisTema.LevettLap = Image.FromFile(ResourcePack.AktualisTema.EleresiUt + "ures" + ".png");

        }

        private void SettingsControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Left + (e.X - PanelMouseDownLocation.X) > 0 &&
                    this.Left + (e.X - PanelMouseDownLocation.X) < Parent.Width - this.Width)
                {
                    this.Left += e.X - PanelMouseDownLocation.X;
                }

                if (this.Top + (e.Y - PanelMouseDownLocation.Y) > 0 &&
                    this.Top + (e.Y - PanelMouseDownLocation.Y) < Parent.Height - this.Height)
                {
                    this.Top += e.Y - PanelMouseDownLocation.Y;
                }

            }
        }

        private void SettingsControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) PanelMouseDownLocation = e.Location;
        }

        private void panelExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void panelExit_MouseEnter(object sender, EventArgs e)
        {
            panelExit.BackColor = Color.LightCoral;
        }

        private void panelExit_MouseLeave(object sender, EventArgs e)
        {
            panelExit.BackColor = Color.Firebrick;
        }

        private void SettingsControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Showed != null && Hided != null)
            {
                if (this.Visible)
                    Showed(sender, EventArgs.Empty);
                else
                    Hided(sender, EventArgs.Empty);
            }
        }

        private void trackBarForditSebesseg_Scroll(object sender, EventArgs e)
        {
            switch (trackBarForditSebesseg.Value)
            {
                case 1:
                    labelFelforditasSebesseg.Text = "Felfordítási sebesség: 0,1 mp";
                    break;
                case 2:
                    labelFelforditasSebesseg.Text = "Felfordítási sebesség: 0,5 mp";
                    break;
                case 3:
                    labelFelforditasSebesseg.Text = "Felfordítási sebesség: 1 mp";
                    break;
                case 4:
                    labelFelforditasSebesseg.Text = "Felfordítási sebesség: 1,5 mp";
                    break;
                case 5:
                    labelFelforditasSebesseg.Text = "Felfordítási sebesség: 2 mp";
                    break;
                default:
                    break;
            }
        }

        private void numericUpDown_SorokSzama_ValueChanged(object sender, EventArgs e)
        {
            ChangedRowColumn(sender, EventArgs.Empty);
        }
    }
}
