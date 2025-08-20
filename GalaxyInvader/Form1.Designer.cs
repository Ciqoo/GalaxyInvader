namespace GalaxyInvader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pGameWindow = new Panel();
            pBPlayBackground = new PictureBox();
            panel1 = new Panel();
            pBMenu = new PictureBox();
            startGameLoop = new System.Windows.Forms.Timer(components);
            panel2 = new Panel();
            pGameWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pBPlayBackground).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pBMenu).BeginInit();
            SuspendLayout();
            // 
            // pGameWindow
            // 
            pGameWindow.Controls.Add(pBPlayBackground);
            pGameWindow.Location = new Point(202, 12);
            pGameWindow.Name = "pGameWindow";
            pGameWindow.Size = new Size(1115, 763);
            pGameWindow.TabIndex = 0;
            // 
            // pBPlayBackground
            // 
            pBPlayBackground.InitialImage = Properties.Resources.GalaxyInvader_Background;
            pBPlayBackground.Location = new Point(0, 0);
            pBPlayBackground.Name = "pBPlayBackground";
            pBPlayBackground.Size = new Size(1115, 763);
            pBPlayBackground.TabIndex = 0;
            pBPlayBackground.TabStop = false;
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(184, 530);
            panel1.TabIndex = 1;
            // 
            // pBMenu
            // 
            pBMenu.BackColor = SystemColors.ActiveCaptionText;
            pBMenu.Location = new Point(12, 12);
            pBMenu.Name = "pBMenu";
            pBMenu.Size = new Size(1304, 764);
            pBMenu.TabIndex = 1;
            pBMenu.TabStop = false;
            // 
            // startGameLoop
            // 
            startGameLoop.Interval = 50;
            startGameLoop.Tick += game_Loop;
            // 
            // panel2
            // 
            panel2.Location = new Point(12, 557);
            panel2.Name = "panel2";
            panel2.Size = new Size(184, 218);
            panel2.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1329, 787);
            Controls.Add(pBMenu);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pGameWindow);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GalaxyInvader";
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            pGameWindow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pBPlayBackground).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBMenu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pGameWindow;
        private Panel panel1;
        private PictureBox pBPlayBackground;
        private PictureBox pBMenu;
        private System.Windows.Forms.Timer startGameLoop;
        private Panel panel2;
    }
}
