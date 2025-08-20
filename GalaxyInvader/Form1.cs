namespace GalaxyInvader
{
    public partial class Form1 : Form
    {
        PictureBox startButton;
        PictureBox[] lifes;
        Label score;
        HelperLib helperLib;
        Game game;

        //User Input Information
        bool userInput_Left = false;
        bool userInput_Right = false;
        bool userInput_Space = false;
        bool userInput_Pause = false;
        bool userInput_Mute = false;
        //Spielstatus
        int gameStatus = 0;

        public Form1()
        {
            InitializeComponent();
            //RaketenIcon f�r das Fenster Setzen.
            this.Icon = Properties.Resources.SpaceShipIcon;
            this.BackColor = Color.Black;
            //Fenstergr��e Fixieren sodass sie nicht ver�ndert werden kann.
            //Da bei �nderungen der Skalierung, das Spielfenster nicht mitskaliert.
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //Option des Maximierung verbieten.
            this.MaximizeBox = false;

            //Hintergrund des Men�s Setzen.
            pBMenu.Image = Properties.Resources.MenuBack;
            pBMenu.SizeMode = PictureBoxSizeMode.StretchImage;
            //Helfer Klasseninstanz auf das Men� erstellen.
            helperLib = new HelperLib(pBMenu);
            //Startbutton vom Men� erstellen.
            startButton = helperLib.initStartButton(pBMenu);
            startButton.Click += menuStartButton_Click;
            startButton.MouseHover += menuStartButton_Hover;
            startButton.MouseLeave += MenuStartButton_Leave;

            //Spielfeld hintergrund in das Hauptfenster einsetzen.
            pBPlayBackground.Parent = pGameWindow;
            pBPlayBackground.Image = Properties.Resources.GalaxyInvader_Background;
            pBPlayBackground.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /**
         * Wird ausgef�hrt, wenn der Mauszeiger den Start Button verl�sst.
         * Das Bild des Sart Buttons wird ausgetauscht zu einem Start button
         * Bild mit normalen Rand.
         */
        private void MenuStartButton_Leave(object? sender, EventArgs e)
        {
            startButton.Image = Properties.Resources.Btn;
        }

        /**
         * Wird ausgef�hrt, wenn der Mauszeiger �ber den Start Button liegt.
         * Das Bild des Sart Buttons wird ausgetauscht zu einem Start button
         * Bild mit wei�em Rand, um die Auswahl zu Highlghiten.
         */
        private void menuStartButton_Hover(object? sender, EventArgs e)
        {
            startButton.Image = Properties.Resources.BtnHover;
        }

        /**
         * Wird ausgef�hrt, wenn der Startbutton geklickt wird.
         * Entfehrnt das Men�, initialisiert die Spiel UI (erstellt Game, Score, Spielanleitung)
         * und erstellt ein Spiel "Game" auf unser Spielfeld.
         * Au�erdem wird der GameLoop gestartet.
         */
        private void menuStartButton_Click(object sender, EventArgs e)
        {
            pBMenu.Enabled = false;
            pBMenu.Visible = false;
            helperLib = new HelperLib(pBPlayBackground);
            game = new Game(pBPlayBackground);
            initScore();
            initLifeMonotoring();
            createUsage();
            startGameLoop.Enabled = true;
        }

        /**
         * Wird ausgef�hrt, wenn eine Taste Gedr�ckt wird.
         * Hier werden Inputs f�r Pfeiltaste(rechts, links), Leertaste und P abgefangen.
         */
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (pBMenu.Enabled == false)
            {
                if (e.KeyCode == Keys.Right)
                {
                    userInput_Right = true;
                }
                if (e.KeyCode == Keys.Left)
                {
                    userInput_Left = true;
                }
                if (e.KeyCode == Keys.Space)
                {
                    userInput_Space = true;
                }
                if (e.KeyCode == Keys.P)
                {
                    userInput_Pause = !userInput_Pause;
                }
                if (e.KeyCode == Keys.M)
                {
                    userInput_Mute = !userInput_Mute;
                }
            }
        }

        /**
         * Wird ausgef�hrt, wenn eine Taste losgelassen wird.
         * Hier werden Inputs f�r Pfeiltaste(rechts, links), Leertaste und P abgefangen.
         */
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (pBMenu.Enabled == false)
            {
                if (e.KeyCode == Keys.Right)
                {
                    userInput_Right = false;
                }
                if (e.KeyCode == Keys.Left)
                {
                    userInput_Left = false;
                }
                if (e.KeyCode == Keys.Space)
                {
                    userInput_Space = false;
                }
            }
        }

        /**
         * Initialisiert die Lebendsanzeige des Spielers.
         */
        private void initLifeMonotoring()
        {
            lifes = new PictureBox[3];
            for (int i = 0; i < lifes.Length; i++)
            {
                lifes[i] = helperLib.initLifeMonitor(i ,panel1);
            }
            
        }

        /**
         * Initialisiert die Scoreanzeige des Spielers.
         */
        private void initScore()
        {
            score = new Label();
            score.Font = new Font("Consolas", 35, FontStyle.Bold);
            score.Text = $"{game.Score}";
            score.BackColor = Color.Black;
            score.ForeColor = Color.White;
            score.TextAlign = ContentAlignment.MiddleCenter;
            score.Location = new Point(panel2.Width / 2 - score.Width, 0);
            score.AutoSize = true;
            panel2.Controls.Add(score);
        }

        /**
         * Erstellt den Close Button im "Game Over" screen.
         */
        private Label createEndButton()
        {
            Label l = new Label();
            l.Font = new Font("Consolas", 55, FontStyle.Bold);
            l.Text = "Close";
            l.BackColor = Color.Gray;
            l.ForeColor = Color.White;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.AutoSize = true;
            l.MouseHover += endButtonHover;
            l.MouseLeave += endButtonLeave;
            l.Click += endButtonClick;

            return l;
        }

        /**
         * Wird ausgef�hrt, wenn der Close Button geklickt wird.
         */
        private void endButtonClick(object? sender, EventArgs e)
        {
            Close();
        }

        /**
         * Wird ausgef�hrt, wenn die Maus den Close Button verl�sst.
         * �ndert farbe f�r das Highlighting.
         */
        private void endButtonLeave(object? sender, EventArgs e)
        {
            Label l = (Label)sender;
            l.BackColor = Color.Gray;
        }

        /**
         * Wird ausgef�hrt, wenn die Maus �ber den Close Button liegt.
         * �ndert farbe f�r das Highlighting.
         */
        private void endButtonHover(object? sender, EventArgs e)
        {
            Label l = (Label) sender;
            l.BackColor = Color.LightGray;
        }

        /**
         * Erstellt eine Spielanleitung, um den Nutzer
         * die Steuerung mitzuteilen.
         */
        private void createUsage()
        {
            Label useage = new Label();
            useage.Font = new Font("Consolas", 10, FontStyle.Bold);
            useage.Text = "Rakete bewegen:\nPfeiltasten (<- | ->)\n\nSchie�en: Leertaste\nStummschalten: M\nPause: P";
            useage.BackColor = Color.Black;
            useage.ForeColor = Color.White;
            useage.TextAlign = ContentAlignment.MiddleCenter;
            useage.Location = new Point(panel2.Width/2 - useage.Width+5, panel2.Height/2);
            useage.AutoSize = true;
            panel2.Controls.Add(useage);
        }

        /**
         * Erstellt den "Game Over" screen.
         */
        private void gameOver()
        {
            Panel pOver = new Panel();
            pOver.BackColor = Color.Black;
            pOver.Size = new Size(this.Width, this.Height);
            this.Controls.Add(pOver);
            pOver.BringToFront();

            score.Parent = pOver;
            score.Font = new Font("Consolas", 55, FontStyle.Bold);
            string s = score.Text;
            score.Text = "Game Over!\n\n";
            score.Text += s;
            score.Location = new Point((pOver.Width / 2) - score.Width/2, (pOver.Height / 2) - score.Height);

            Label endButton = createEndButton();

            endButton.Parent = pOver;
            endButton.Location = new Point((pOver.Width / 2) - endButton.Width / 2, (pOver.Height / 2) + 2*endButton.Height);

        }

        /**
         * Akutalisiert die Lebendsanzeige des Spielers.
         */
        private void updateLifeMonitoring()
        {
            if (game.Player.getLife() == 2)
            {
                lifes[0].Image = Properties.Resources.LifeBroke;
            }
            if (game.Player.getLife() == 1)
            {
                lifes[1].Image = Properties.Resources.LifeBroke;
            }
            if (game.Player.getLife() == 0)
            {
                lifes[2].Image = Properties.Resources.LifeBroke;
            }
        }

        /**
         * Aktualisiert die Scoreanzeige des Spielers.
         */
        private void updateScore()
        {
            score.Text = $"Score:\n {game.Score}";
        }

        /**
         * GAME LOOP
         * Wird durch den Timer gesteuert.
         * W�hrend das Spiel l�uft wird diese Funktion immer und immer
         * wieder aufgerufen, um den status des Spiels und die angezeigten
         * Elemente zu aktualisieren.
         */
        private void game_Loop(object sender, EventArgs e)
        {
            if(gameStatus == 0)
            {
                gameStatus = game.updateInterval(userInput_Space, userInput_Left, userInput_Right, userInput_Pause, userInput_Mute);
                updateScore();
                updateLifeMonitoring();
            }
            else
            {
                startGameLoop.Stop();
                gameOver();                          
            }
                
        }
    }
}
