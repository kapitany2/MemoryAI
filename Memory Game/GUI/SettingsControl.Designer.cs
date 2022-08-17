
namespace Memory_Game.GUI
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_Theme = new System.Windows.Forms.ComboBox();
            this.label_Theme = new System.Windows.Forms.Label();
            this.label_SorOszlopSzam = new System.Windows.Forms.Label();
            this.numericUpDown_SorokSzama = new System.Windows.Forms.NumericUpDown();
            this.button_Save = new System.Windows.Forms.Button();
            this.panelExit = new System.Windows.Forms.Panel();
            this.labelFelforditasSebesseg = new System.Windows.Forms.Label();
            this.trackBarForditSebesseg = new System.Windows.Forms.TrackBar();
            this.labelInfo = new System.Windows.Forms.Label();
            this.checkBox_StartAI = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SorokSzama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForditSebesseg)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_Theme
            // 
            this.comboBox_Theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Theme.FormattingEnabled = true;
            this.comboBox_Theme.Location = new System.Drawing.Point(54, 29);
            this.comboBox_Theme.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox_Theme.Name = "comboBox_Theme";
            this.comboBox_Theme.Size = new System.Drawing.Size(193, 21);
            this.comboBox_Theme.TabIndex = 0;
            this.comboBox_Theme.SelectedIndexChanged += new System.EventHandler(this.comboBox_Theme_SelectedIndexChanged);
            // 
            // label_Theme
            // 
            this.label_Theme.AutoSize = true;
            this.label_Theme.Location = new System.Drawing.Point(2, 32);
            this.label_Theme.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Theme.Name = "label_Theme";
            this.label_Theme.Size = new System.Drawing.Size(37, 13);
            this.label_Theme.TabIndex = 1;
            this.label_Theme.Text = "Téma:";
            // 
            // label_SorOszlopSzam
            // 
            this.label_SorOszlopSzam.AutoSize = true;
            this.label_SorOszlopSzam.Location = new System.Drawing.Point(2, 55);
            this.label_SorOszlopSzam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SorOszlopSzam.Name = "label_SorOszlopSzam";
            this.label_SorOszlopSzam.Size = new System.Drawing.Size(94, 13);
            this.label_SorOszlopSzam.TabIndex = 2;
            this.label_SorOszlopSzam.Text = "Sor / oszlop szám:";
            // 
            // numericUpDown_SorokSzama
            // 
            this.numericUpDown_SorokSzama.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown_SorokSzama.Location = new System.Drawing.Point(190, 54);
            this.numericUpDown_SorokSzama.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_SorokSzama.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_SorokSzama.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown_SorokSzama.Name = "numericUpDown_SorokSzama";
            this.numericUpDown_SorokSzama.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown_SorokSzama.TabIndex = 3;
            this.numericUpDown_SorokSzama.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown_SorokSzama.ValueChanged += new System.EventHandler(this.numericUpDown_SorokSzama_ValueChanged);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(54, 141);
            this.button_Save.Margin = new System.Windows.Forms.Padding(2);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(133, 32);
            this.button_Save.TabIndex = 4;
            this.button_Save.Text = "Mentés és új játék";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // panelExit
            // 
            this.panelExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExit.BackColor = System.Drawing.Color.Firebrick;
            this.panelExit.Location = new System.Drawing.Point(225, 2);
            this.panelExit.Margin = new System.Windows.Forms.Padding(2);
            this.panelExit.Name = "panelExit";
            this.panelExit.Size = new System.Drawing.Size(21, 22);
            this.panelExit.TabIndex = 5;
            this.panelExit.Click += new System.EventHandler(this.panelExit_Click);
            this.panelExit.MouseEnter += new System.EventHandler(this.panelExit_MouseEnter);
            this.panelExit.MouseLeave += new System.EventHandler(this.panelExit_MouseLeave);
            // 
            // labelFelforditasSebesseg
            // 
            this.labelFelforditasSebesseg.AutoSize = true;
            this.labelFelforditasSebesseg.Location = new System.Drawing.Point(4, 77);
            this.labelFelforditasSebesseg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFelforditasSebesseg.Name = "labelFelforditasSebesseg";
            this.labelFelforditasSebesseg.Size = new System.Drawing.Size(110, 13);
            this.labelFelforditasSebesseg.TabIndex = 6;
            this.labelFelforditasSebesseg.Text = "Felfordítási sebesség:";
            // 
            // trackBarForditSebesseg
            // 
            this.trackBarForditSebesseg.Location = new System.Drawing.Point(168, 76);
            this.trackBarForditSebesseg.Margin = new System.Windows.Forms.Padding(2);
            this.trackBarForditSebesseg.Maximum = 5;
            this.trackBarForditSebesseg.Minimum = 1;
            this.trackBarForditSebesseg.Name = "trackBarForditSebesseg";
            this.trackBarForditSebesseg.Size = new System.Drawing.Size(78, 45);
            this.trackBarForditSebesseg.TabIndex = 7;
            this.trackBarForditSebesseg.Value = 3;
            this.trackBarForditSebesseg.Scroll += new System.EventHandler(this.trackBarForditSebesseg_Scroll);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.ForeColor = System.Drawing.Color.Red;
            this.labelInfo.Location = new System.Drawing.Point(16, 108);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 8;
            // 
            // checkBox_StartAI
            // 
            this.checkBox_StartAI.AutoSize = true;
            this.checkBox_StartAI.Checked = true;
            this.checkBox_StartAI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_StartAI.Location = new System.Drawing.Point(81, 119);
            this.checkBox_StartAI.Name = "checkBox_StartAI";
            this.checkBox_StartAI.Size = new System.Drawing.Size(73, 17);
            this.checkBox_StartAI.TabIndex = 9;
            this.checkBox_StartAI.Text = "MI indítás";
            this.checkBox_StartAI.UseVisualStyleBackColor = true;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.checkBox_StartAI);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.trackBarForditSebesseg);
            this.Controls.Add(this.labelFelforditasSebesseg);
            this.Controls.Add(this.panelExit);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.numericUpDown_SorokSzama);
            this.Controls.Add(this.label_SorOszlopSzam);
            this.Controls.Add(this.label_Theme);
            this.Controls.Add(this.comboBox_Theme);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(248, 184);
            this.VisibleChanged += new System.EventHandler(this.SettingsControl_VisibleChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SettingsControl_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SorokSzama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarForditSebesseg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_Theme;
        private System.Windows.Forms.Label label_SorOszlopSzam;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Panel panelExit;
        internal System.Windows.Forms.ComboBox comboBox_Theme;
        internal System.Windows.Forms.NumericUpDown numericUpDown_SorokSzama;
        private System.Windows.Forms.Label labelFelforditasSebesseg;
        internal System.Windows.Forms.TrackBar trackBarForditSebesseg;
        private System.Windows.Forms.Label labelInfo;
        internal System.Windows.Forms.CheckBox checkBox_StartAI;
    }
}
