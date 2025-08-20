using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Player, stellt den Spieler des Spiels dar.
     */
    public class Player : IKillable
    {
        PictureBox gameField;
        Position position;
        PictureBox image;
        Inventory inventory;
        PictureBox thruster;
        int currentWeapon;

        //Statische Werte. Spieler hat Maximal 3 Leben.
        int lifes = 3;
        //Geschwindigkeit des Spielers beim Bewegungsinterval.
        int speed = 20;

        //Getter - Setter
        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Inventory Inventory
        {
            get { return this.inventory; }
            set { this.inventory = value; }
        }

        public int CurrentWeapon
        {
            get { return this.currentWeapon; }
            set { this.currentWeapon = value; }
        }

        public int Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }

        public PictureBox Image
        {
            get { return this.image; }
            set { this.image = value; }
        }


        /**
         * Konstruktor für den Spieler
         * @param gameField - Parent Object auf das der Spieler Objecte setzen kann.
         */
        public Player(PictureBox gameField)
        {
            inventory = new Inventory();
            this.image = HelperLib.initStartRocket(gameField);
            this.position = HelperLib.convertImageLocationToPosition(this.Image);
            this.currentWeapon = 0;
            this.gameField = gameField;

            this.thruster = HelperLib.createThruster(this.position, image);
        }

        /**
         * Spieler schießt und erzeugt somit ein Projektil.
         */
        public void shoot()
        {
            Position top = new Position(this.position.X, this.position.Y);
            top.incY(-1 * (this.image.Height / 2));
            this.inventory.weapons[this.currentWeapon].projectiles.Add(new Projectile(0, top, this.gameField));
        }

        /**
         * Bewegt die Projektiktile des Spielers in die Y Richtung.
         * @param offset - Geschwindigkeit der Projektile.
         */
        public void updateProjectiles(int offset)
        {

            for (int i = 0; i < this.inventory.weapons[currentWeapon].projectiles.Count; i++)
            {
                this.inventory.weapons[currentWeapon].projectiles[i].position.Y -= offset;
                this.inventory.weapons[currentWeapon].projectiles[i].syncProjectile();
                int yBound = this.inventory.weapons[currentWeapon].projectiles[i].position.Y;
                if (yBound <= 0 || yBound >= gameField.Height)
                {
                    this.inventory.weapons[currentWeapon].projectiles[i].disposeProjectile();
                    this.inventory.weapons[currentWeapon].projectiles.RemoveAt(i);
                }
            }
        }

        /**
         * Synchroniesiert die Spieler Position mit der Spieler Bild Positon
         * und die Schubdüsen Position.
         */
        public void syncPlayerImage()
        {
            HelperLib.convertPositionToImageLocation(this.image, this.position);
            Position t = new Position(this.image.Width / 2, (this.image.Height / 2) + this.thruster.Height / 2);
            HelperLib.convertPositionToImageLocation(this.thruster, t);
        }

        /**
         * Gibt zurück ob der Spieler keine Leben mehr hat.
         * @out true - false
         */
        public bool isKilled() => this.lifes > 0 ? false : true;

        /**
         * Gibt die anzahl an Leben des Spielers zurück.
         */
        public int getLife() => this.lifes;

        /**
         * Zieht ein Leben des Spielers ab.
         */
        public void subLife()
        {
            if (this.lifes > 0)
            {
                this.lifes--;
            }
        }
        /**
         * Fügt dem Spieler ein Leben hinzu.
         */
        public void addLife()
        {
            this.lifes++;
        }
    }


}
