using System.Diagnostics;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GalaxyInvader
{
    /*
     * Klasse HelperLib um helfer Funktionen zu erstellen, sodass
     * die Klassen nicht zu voll werden.
     */
    public class HelperLib
    {
        //Parent Element, auf dem Bilder standardmäßig gesetzt werden.
        private PictureBox parent;

        /**
         * Konstruktor einer HelpferLib.
         * @param parent - Parent Element auf dem standardmäßig Bilder gesetzt werden.
         */
        public HelperLib(PictureBox parent)
        {
            this.parent = parent;
        }

        /**
         * Setzt ein skaliertes Bild mit transparenten Hintergrund auf das Parent Attribut.
         * @paran pic - PictureBox die mit Bild befüllt werden und auf das Parent Attribut gesetzt werden soll.
         * @param img - Bild, mit dem die PictureBox Gefüllt werden soll.
         */
        public void intitPicture(PictureBox pic, Image img)
        {
            pic.Parent = this.parent;
            pic.BackColor = Color.Transparent;
            pic.Image = img;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /**
         * STATISCHE VERSION VON initPicture()
         * Setzt ein skaliertes Bild mit transparenten Hintergrund auf das Parent.
         * @paran pic - PictureBox die mit Bild befüllt werden und auf das Parent gesetzt werden soll.
         * @param img - Bild, mit dem die PictureBox Gefüllt werden soll.
         */
        public static void intitPicture(PictureBox pic, Image img, PictureBox parent)
        {
            pic.Parent = parent;
            pic.BackColor = Color.Transparent;
            pic.Image = img;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /**
         * Erstellt eine PictureBox mit Spieler Bild (Rakete). Diese wird skaliert und
         * auf die Startposition gesetzt.
         * @param parent - Parent Element, auf dem die Rakete gesetzt werden soll.
         * @out PictureBox mit richtiger Startposition und richtigem Bild.
         */
        public static PictureBox initStartRocket(PictureBox parent)
        {
            PictureBox player = new PictureBox();

            player.Size = new Size(100, 100);
            player.Location = new Point(parent.Width / 2 - player.Width / 2, parent.Height - (100 + 30)); //+30 als Offset für abstand zum boden.
            HelperLib.intitPicture(player, Properties.Resources.SpaceShip, parent);

            return player;
        }


        /**
         * Erstellt ein Herz Symbol für die Lebensanzeige des Spielers.
         * Je nach index weicht die Y Position ab. (Max. anzahl der herzen des Spielers == 3).
         * @param index - Index position der Herzenanzeige, welches erstellt werden soll.
         * @param parent - Parent Element, auf dem die Herzanzeige gesetzt werden soll.
         * @out Herzenanzeige an der Indexposition index.
         */
        public PictureBox initLifeMonitor(int index, Panel parent)
        {
            PictureBox lifeMonitor = new PictureBox();
            lifeMonitor.Size = new Size(100, 100);
            lifeMonitor.Location = new Point(parent.Width / 2 - lifeMonitor.Width / 2, parent.Height / 3 * index);//*3 index da es Maximal 3 gibt

            lifeMonitor.Parent = parent;
            lifeMonitor.BackColor = Color.Transparent;
            lifeMonitor.Image = Properties.Resources.Life;
            lifeMonitor.SizeMode = PictureBoxSizeMode.StretchImage;

            return lifeMonitor;
        }


        /**
         * Erstellt den Startbutton des Spiels mit festgesetzter Position und Größe.
         * @param parent - Parent Element, auf das der Button gesetzt werden soll.
         * @out Startbutton der erstellt und auf parent gesetzt wurde.
         */
        public PictureBox initStartButton(PictureBox parent)
        {

            PictureBox startButton = new PictureBox();
            intitPicture(startButton, Properties.Resources.Btn);
            startButton.Location = new Point((this.parent.Width / 2) - 180, (this.parent.Height / 2) - 50);
            startButton.Size = new Size(357, 106);

            return startButton;
        }

        /**
         * Wandelt die Bildposition zu einer Position des Mittelpunkts des Bildes um,
         * da die Bildposition immer die obere linke ecke des bildes definiert.
         * @param pic - Bild dessen Mittelpunkt ermittelt werden soll.
         * @out Position des Mittelpunkts des Bildes.
         */
        public static Position convertImageLocationToPosition(PictureBox pic)
        {
            Position position = new Position(
            pic.Location.X + (pic.Width / 2),
            pic.Location.Y + (pic.Height / 2)
            );

            return position;
        }

        /**
         * Setzt ein Bild auf eine Position, wobei die Position den Mittelpunkt angibt.
         * @param pic - Bild, welches auf eine Position geschoben wird.
         * @param pos - Position, auf die das Bild geschoben werden soll.
         */
        public static void convertPositionToImageLocation(PictureBox pic, Position pos)
        {
            pic.Location = new Point(pos.X - (pic.Width / 2), pos.Y - (pic.Height / 2));
        }

        /**
         * Erstellt ein Bild eines bestimmten Projektils auf ein Parent.
         * @param v - variante des Projektils (optisch - anderes Bild).
         * @param parent - Parent Element, auf dem das Projektil gesetzt werden soll.
         * @out Bild Object des erstellten Projektils.
         */
        public static PictureBox createProjectile(int v, PictureBox parent)
        {
            Image i = Properties.Resources.Projectile1;

            switch (v)
            {
                case 0:
                    i = Properties.Resources.Projectile1;
                    break;
                case 1:
                    i = Properties.Resources.Projectile2;
                    break;
            }

            PictureBox projectile = new PictureBox();

            projectile.Parent = parent;
            projectile.BackColor = Color.Transparent;
            projectile.Image = i;
            projectile.SizeMode = PictureBoxSizeMode.StretchImage;
            projectile.Size = new Size(25, 25);

            return projectile;
        }

        /**
         * Erstellt ein Bild eines bestimmten Gegners auf ein Parent.
         * @param v - variante des Gegners (optisch - anderes Bild).
         * @param parent - Parent Element, auf dem der Gegner gesetzt werden soll.
         * @out Bild Object des erstellten Gegners.
         */
        public static PictureBox createEnemy(int v, PictureBox parent)
        {
            Image i = Properties.Resources.Enemy1;

            switch (v)
            {
                case 0:
                    i = Properties.Resources.Enemy1;
                    break;
            }

            PictureBox enemy = new PictureBox();

            enemy.Parent = parent;
            enemy.BackColor = Color.Transparent;
            enemy.Image = i;
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy.Size = new Size(100, 100);

            return enemy;
        }

        /**
         * Erstellt eine optische ExplosionsAnimation mithilfe eines Gif Bildes.
         * @param p - Position an der sich die ExplosionsAnimation abspielen soll.
         * @param parent - Parent Element, auf das die Explosion gesetzt werden soll.
         * @out optische Bewegbildkomponente der ExplosionsAnimation.
         */
        public static PictureBox createExplodeAnimation(Position p, PictureBox parent)
        {
            PictureBox explode = new PictureBox();

            explode.Parent = parent;
            explode.BackColor = Color.Transparent;
            explode.Image = Properties.Resources.Explode;
            explode.SizeMode = PictureBoxSizeMode.StretchImage;
            explode.Size = new Size(100, 100);
            HelperLib.convertPositionToImageLocation(explode, p);

            explode.BringToFront();

            return explode;
        }

        /**
         * Erstellt die optischen Schubdüsen mithilfe eines Gif Bildes.
         * @param p - Position an der sich die Schubdüsen befinden sollen.
         * @param parent - Parent Element, auf das die Schubdüsen gesetzt werden sollen.
         * @out optische Bewegbildkomponente der Schubdüsen.
         */
        public static PictureBox createThruster(Position p, PictureBox parent)
        {
            PictureBox thruster = new PictureBox();

            thruster.Parent = parent;
            thruster.BackColor = Color.Transparent;
            thruster.Image = Properties.Resources.Thruster;
            thruster.SizeMode = PictureBoxSizeMode.StretchImage;
            thruster.Size = new Size(80, 80);
            Position t = new Position(parent.Width / 2, (parent.Height / 2) + thruster.Height / 2);
            HelperLib.convertPositionToImageLocation(thruster, t);

            return thruster;
        }

        /**
         * Berechnet den Winkel zwischen zwei Positionen.
         * NOTIZ: Ursprünglich wollte ich diese zum drehen von Spiel Elementen verwenden. Leider
         * unterstützt die PictureBox keine Funktion, sich zu drehen,
         * weshalb die Funktion calculateAngle() nicht verwendet wird.
         * @param p1 - Position 1 zur berechnung des Winkels.
         * @paran p2 - Position 2 zu berechnung des Winkels.
         */
        public static double calculateAngle(Position p1, Position p2)
        {
            double angle = 0;

            double scalar = p1.X * p2.X + p1.Y * p2.Y;
            double lengthP1 = Math.Sqrt(Math.Pow(p1.X, 2) + Math.Pow(p1.Y, 2));
            double lengthP2 = Math.Sqrt(Math.Pow(p2.X, 2) + Math.Pow(p2.Y, 2));
            double value = scalar / lengthP1 * lengthP2;

            double calcCos = Math.Acos(value);
            angle = calcCos * 180 / Math.PI;

            return angle;
        }

        /**
         * Erstellt ein Array in dem passende X Positionen für die Spawnpunkte der
         * Gegner enthalten sind.
         * @out Array mit passenden X Positionen für Gegner.
         */
        public static int[] spawnHelperArrayX()
        {
            int[] pos = new int[9];
            pos[0] = 1050;
            pos[1] = 930;
            pos[2] = 810;
            pos[3] = 690;
            pos[4] = 570;
            pos[5] = 450;
            pos[6] = 330;
            pos[7] = 210;
            pos[8] = 90;

            return pos;
        }

        public static void playSound(bool b, Stream? stream)
        {
            if(b)
            {
                System.Media.SoundPlayer sound = new System.Media.SoundPlayer(stream);
                sound.Play();
            }   
        }


    }

}