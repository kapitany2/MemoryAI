
namespace Memory_Game.GUI
{
    partial class AISettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_Debug = new System.Windows.Forms.CheckBox();
            this.checkBox_ConsoleVisibility = new System.Windows.Forms.CheckBox();
            this.numericUpDownHashLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_AITimer = new System.Windows.Forms.NumericUpDown();
            this.label_AI_Sebesseg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHashLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AITimer)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_Debug
            // 
            this.checkBox_Debug.AutoSize = true;
            this.checkBox_Debug.Location = new System.Drawing.Point(9, 54);
            this.checkBox_Debug.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_Debug.Name = "checkBox_Debug";
            this.checkBox_Debug.Size = new System.Drawing.Size(58, 17);
            this.checkBox_Debug.TabIndex = 1;
            this.checkBox_Debug.Text = "Debug";
            this.checkBox_Debug.UseVisualStyleBackColor = true;
            this.checkBox_Debug.CheckedChanged += new System.EventHandler(this.checkBox_Debug_CheckedChanged);
            // 
            // checkBox_ConsoleVisibility
            // 
            this.checkBox_ConsoleVisibility.AutoSize = true;
            this.checkBox_ConsoleVisibility.Location = new System.Drawing.Point(9, 92);
            this.checkBox_ConsoleVisibility.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_ConsoleVisibility.Name = "checkBox_ConsoleVisibility";
            this.checkBox_ConsoleVisibility.Size = new System.Drawing.Size(121, 17);
            this.checkBox_ConsoleVisibility.TabIndex = 2;
            this.checkBox_ConsoleVisibility.Text = "Console Show/Hide";
            this.checkBox_ConsoleVisibility.UseVisualStyleBackColor = true;
            this.checkBox_ConsoleVisibility.CheckedChanged += new System.EventHandler(this.checkBox_ConsoleVisibility_CheckedChanged);
            // 
            // numericUpDownHashLength
            // 
            this.numericUpDownHashLength.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownHashLength.Location = new System.Drawing.Point(190, 15);
            this.numericUpDownHashLength.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownHashLength.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownHashLength.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownHashLength.Name = "numericUpDownHashLength";
            this.numericUpDownHashLength.Size = new System.Drawing.Size(67, 20);
            this.numericUpDownHashLength.TabIndex = 0;
            this.numericUpDownHashLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownHashLength.ValueChanged += new System.EventHandler(this.numericUpDownHashLength_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mintavétel mérete:";
            // 
            // numericUpDown_AITimer
            // 
            this.numericUpDown_AITimer.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_AITimer.Location = new System.Drawing.Point(167, 183);
            this.numericUpDown_AITimer.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_AITimer.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown_AITimer.Name = "numericUpDown_AITimer";
            this.numericUpDown_AITimer.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_AITimer.TabIndex = 12;
            this.numericUpDown_AITimer.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_AITimer.ValueChanged += new System.EventHandler(this.numericUpDown_AITimer_ValueChanged);
            // 
            // label_AI_Sebesseg
            // 
            this.label_AI_Sebesseg.AutoSize = true;
            this.label_AI_Sebesseg.Location = new System.Drawing.Point(12, 185);
            this.label_AI_Sebesseg.Name = "label_AI_Sebesseg";
            this.label_AI_Sebesseg.Size = new System.Drawing.Size(100, 13);
            this.label_AI_Sebesseg.TabIndex = 13;
            this.label_AI_Sebesseg.Text = "Sebesség: 1000 ms";
            // 
            // AISettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 284);
            this.Controls.Add(this.label_AI_Sebesseg);
            this.Controls.Add(this.numericUpDown_AITimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownHashLength);
            this.Controls.Add(this.checkBox_ConsoleVisibility);
            this.Controls.Add(this.checkBox_Debug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AISettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AI Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AISettingsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHashLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AITimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_Debug;
        private System.Windows.Forms.CheckBox checkBox_ConsoleVisibility;
        private System.Windows.Forms.NumericUpDown numericUpDownHashLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_AITimer;
        private System.Windows.Forms.Label label_AI_Sebesseg;
    }
}