using Memory_Game.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Memory_Game.Debug;

namespace Memory_Game.GUI
{
    public partial class AISettingsForm : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public EventHandler Debug;
        public EventHandler Visibility;
        public EventHandler TimerIntervalChanged;
        public AISettingsForm()
        {
            InitializeComponent();
        }

        private void AISettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void numericUpDownHashLength_ValueChanged(object sender, EventArgs e)
        {
            ImageHash.HashSize = (int)numericUpDownHashLength.Value;
        }

        private void checkBox_Debug_CheckedChanged(object sender, EventArgs e)
        {
            Debug(sender, EventArgs.Empty);
            DConsole.kiir = checkBox_Debug.Checked;
            DConsole.WriteLine("Console kiíratás: " + checkBox_Debug.Checked);
        }

        private void checkBox_ConsoleVisibility_CheckedChanged(object sender, EventArgs e)
        {
            Visibility(sender, EventArgs.Empty);
            var handle = GetConsoleWindow();

            ShowWindow(handle, checkBox_ConsoleVisibility.Checked ? SW_SHOW : SW_HIDE);
        }

        private void numericUpDown_AITimer_ValueChanged(object sender, EventArgs e)
        {
            TimerIntervalChanged(sender, e);
            label_AI_Sebesseg.Text = "Sebesség: " + numericUpDown_AITimer.Value + " ms";
        }
    }
}
