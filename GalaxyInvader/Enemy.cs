using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Enemy definiert einen Gegner in dem Spiel.
     */
    public class Enemy : IKillable
    {
        //Feuerrate eines Gegners. Hier statisch gesetzt. Fügt man neue Gegner ein, dann man den
        //als Variable im Konstruktor setzen um unterschiedliche Gegner zu erstellen.
        int enemyFireRate = 70;

        //Definiert die Anzahl an Leben die der Gegner hat.
        int lifes;

        Position position;
        Weapon weapon;
        PictureBox image;
        Random rdm = new Random();


        //Getter - Setter
        public PictureBox Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Weapon Weapon
        {
            get { return this.weapon; }
            set { this.weapon = value; }
        }



        /**
         * Konstruktor eines Gegners.
         * @param image - PictureBox mit enthaltenen skalierten Bild des neu erstellten Gegners
         * @param position - Position des neu erstellten Gegners
         * @param lifes - Anzahl der leben des neu erstellten Gegners
         */
        public Enemy(PictureBox image, Position position, int lifes)
        {
            this.image = image;
            this.position = position;
            syncEnemy();
            this.weapon = new Weapon(enemyFireRate);
            this.lifes = lifes;
        }

        /**
         * Überladener Konstruktor eines Gegners, bei dem man auch die Feuerrate einstellen kann.
         * @param image - PictureBox mit enthaltenen skalierten Bild des neu erstellten Gegners
         * @param position - Position des neu erstellten Gegners
         * @param lifes - Anzahl der leben des neu erstellten Gegners
         * @param firerate - Feuerrate (Angriffsinterval)
         */
        public Enemy(PictureBox image, Position position, int lifes, int firerate)
        {
            this.image = image;
            this.position = position;
            syncEnemy();
            this.weapon = new Weapon(enemyFireRate);
            this.lifes = lifes;
            this.enemyFireRate = firerate;
        }

        /**
         * Synchronisiert die Angezeigte Bild Position des Gegners mit seiner Position.
         */
        public void syncEnemy()
        {
            HelperLib.convertPositionToImageLocation(this.image, this.position);
        }

        /**
         * Entfernt das angezeigte Bild des Gegners.
         */
        public void disposeEnemy()
        {
            this.image.Dispose();
        }

        /**
         * Gegner schießt in richtung einer Position.
         * @param parent - Parent Element, auf dem das Projektil gesetzt werden soll (Spielfeld).
         * @param pos - Position auf die der Gegner schießt.
         */
        public void shoot(PictureBox parent, Position pos)
        {
            //Position des Gegners == Mittelpunkt vom Gegner Bild. Das Projektil soll unter dem Gegner Spawnen.
            //Also Y + halbe höhe vom Gegner Bild.
            Position bot = new Position(this.position.X, this.position.Y);
            bot.incY(this.image.Height / 2);

            Projectile p = new Projectile(1, bot, parent);
            //Berechnung des Richtungsvektors für die Flugrichtung des Projektils.
            p.destination.X = (pos.X + rdm.Next(-40, 40)) - this.position.X;
            p.destination.Y = pos.Y - this.position.Y;

            this.weapon.projectiles.Add(p);
        }

        /**
         * Bewegt die Projektile in ihre Richtung, mit einer bestimmten Geschwindigkeit.
         * @param offset - Geschwindigkeit der Projektile bzw. gewegungs schritt größe pro Interval
         * @param parent - Parent Element. Wird verwendet um zu schauen, ob ein Projektil außerhalb der Map ist.
         */
        public void updateProjectiles(int offset, PictureBox parent)
        {

            for (int i = 0; i < this.weapon.projectiles.Count; i++)
            {
                this.weapon.projectiles[i].position.Y += this.weapon.projectiles[i].destination.Y / offset;
                this.weapon.projectiles[i].position.X += this.weapon.projectiles[i].destination.X / offset;
                this.weapon.projectiles[i].syncProjectile();
                int yBound = this.weapon.projectiles[i].position.Y;
                if (yBound >= parent.Height)
                {
                    this.weapon.projectiles[i].disposeProjectile();
                    this.weapon.projectiles.RemoveAt(i);
                }
            }
        }

        /**
         * Prüft ob der gegner gestorben ist.
         * @out true - false.
         */
        public bool isKilled() => this.lifes > 0 ? false : true;

        /**
         * Gibt die anzahl an Leben des Gegners zurück.
         * @out anzahl an leben.
         */
        public int getLife() => this.lifes;

        public void setLife(int x)
        {
            this.lifes = x;
        }

        /**
         * Zieht ein Leben des Gegners ab.
         */
        public void subLife()
        {
            if (this.lifes > 0)
            {
                this.lifes--;
            }
        }
        /**
         * Fügt ein Leben hinzu.
         */
        public void addLife()
        {
            this.lifes++;
        }
    }
}
