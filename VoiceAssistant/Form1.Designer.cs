namespace VoiceAssistant
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.speaking_Timer = new System.Windows.Forms.Timer(this.components);
            this.showCommands_lstBx = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(116, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Live Speech";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(116, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BotSpeaks";
            // 
            // speaking_Timer
            // 
            this.speaking_Timer.Enabled = true;
            this.speaking_Timer.Interval = 1000;
            this.speaking_Timer.Tick += new System.EventHandler(this.Speaking_Timer_Tick);
            // 
            // showCommands_lstBx
            // 
            this.showCommands_lstBx.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.showCommands_lstBx.Dock = System.Windows.Forms.DockStyle.Right;
            this.showCommands_lstBx.ForeColor = System.Drawing.SystemColors.Window;
            this.showCommands_lstBx.FormattingEnabled = true;
            this.showCommands_lstBx.Location = new System.Drawing.Point(586, 0);
            this.showCommands_lstBx.Name = "showCommands_lstBx";
            this.showCommands_lstBx.Size = new System.Drawing.Size(214, 450);
            this.showCommands_lstBx.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.showCommands_lstBx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer speaking_Timer;
        public System.Windows.Forms.ListBox showCommands_lstBx;
    }
}

