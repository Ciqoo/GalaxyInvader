using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Game stellt ein Spiel dar.
     */
    public class Game
    {
        PictureBox gameField;
        Player player;
        Enemies enemies;
        bool pause;
        bool gameOver;
        int score;
        bool soundActive = true;

        //Zur messung der Spawnrate von Gegnern
        int spawnTimer = 0;
        //Variable zur Anpassung der Spiel schwierigkeit. Wird dymanisch angepasst.
        int difficulty = 1;

        //Getter - Setter
        public Player Player
        {
            get { return this.player; }
            set { this.player = value; }
        }
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        /**
         * Konstruktor einer Spiel Instanz.
         * @param gameField - Container, der als Spielfläche verwendet werden soll.
         */
        public Game(PictureBox gameField)
        {
            this.player = new Player(gameField);
            this.gameField = gameField;
            this.enemies = new Enemies();
            this.pause = false;
            this.score = 0;
            this.gameOver = false;
        }

        /**
         * Löscht alle zu löschenden Spielelemente. (Alle gegner die 0 Leben haben oder deaktivierte Projektile)
         */
        private void clearDisabled()
        {
            //Löschen aller deaktivierten Projektile des Spielers.
            for (int i = this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles.Count - 1; i >= 0; i--)
            {
                if (this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles[i].disable == true)
                {
                    this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles[i].disposeProjectile();
                    this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles.RemoveAt(i);
                }
            }

            //Löschen aller deaktivierten Projektile aller Gegner.
            for (int k = this.enemies.enemies.Count - 1; k >= 0; k--)
            {
                for (int l = this.enemies.enemies[k].Weapon.projectiles.Count - 1; l >= 0; l--)
                {
                    if (this.enemies.enemies[k].Weapon.projectiles[l].disable == true)
                    {
                        this.enemies.enemies[k].Weapon.projectiles[l].disposeProjectile();
                        this.enemies.enemies[k].Weapon.projectiles.RemoveAt(l);
                    }
                }
            }

            //Löschen aller Gegner die 0 Leben haben.
            for (int j = this.enemies.enemies.Count - 1; j >= 0; j--)
            {
                if (this.enemies.enemies[j].getLife() == 0)
                {
                    this.score++;
                    updateDifficulty();
                    this.enemies.killEnemyAt(j, gameField);
                    HelperLib.playSound(soundActive, Properties.Resources.Explode1);
                }
            }
        }

        /**
         * Prüft ob ein Gegner in den Spieler rein fliegt. Ist das der Fall,
         * so wird der Gegner zerstört und der Spieler verliert ein Leben.
         */
        private void checkPlayerCollision()
        {
            for (int j = 0; j < this.enemies.enemies.Count; j++)
            {
                if (this.enemies.enemies[j].Image.Bounds.IntersectsWith(this.player.Image.Bounds))
                {
                    this.enemies.enemies[j].setLife(0);
                    this.player.subLife();
                }
            }
        }

        /**
         * Prüft ob das Spiel vorbei ist. Das ist der Fall, wenn der Spieler 0 Leben hat.
         */
        private void checkGameOver()
        {
            if (this.Player.getLife() == 0)
            {
                gameOver = true;
            }
        }

        /**
         * Prüft für jedes Projektil des Spielers, irgend einer der Gegner getroffen wird.
         * Ist das der fall, wird dem getroffenen Gegner ein Leben abgezogen und das
         * Spielerprojektil deaktiviert.
         */
        private void checkProjectileCollision()
        {
            for (int i = 0; i < this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles.Count; i++)
            {
                for (int j = 0; j < this.enemies.enemies.Count; j++)
                {
                    if (this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles[i].image.Bounds.IntersectsWith(
                       this.enemies.enemies[j].Image.Bounds
                       ))
                    {
                        this.enemies.enemies[j].subLife();
                        this.player.Inventory.weapons[this.player.CurrentWeapon].projectiles[i].disable = true;
                    }
                }
            }
        }

        /**
         * Regelt das Angriffs verhalten der Gegner. In welchen Bereich die Gegner schießen
         * und ob die Waffe der Gegner überhaupt schussbereit ist (Waffen cooldown für Schussinterval).
         */
        private void enemyShootManagement()
        {
            //Gegner schießen wenn sie 1/10 der Map überschritten haben und
            //Hören auf, wenn sie bei der hälfte der Map angekommen sind.
            int distanceMin = this.gameField.Height / 10;
            int distanceMax = this.gameField.Height / 2;
            for (int i = 0; i < this.enemies.enemies.Count; i++)
            {
                if (this.enemies.enemies[i].Position.Y > distanceMin &&
                    this.enemies.enemies[i].Position.Y < distanceMax &&
                    this.enemies.enemies[i].Weapon.isReadyToShoot())
                {
                    this.enemies.enemies[i].shoot(gameField, this.player.Position);
                    this.enemies.enemies[i].Weapon.setCooldown();
                    HelperLib.playSound(soundActive, Properties.Resources.Shoot2);
                }
                this.enemies.enemies[i].Weapon.tickCooldown();
                this.enemies.enemies[i].updateProjectiles(50, gameField);
            }
        }

        /**
         * Prüft ob einer der Projektile aller Gegner den Spieler trifft.
         * Ist das der Fall, wird das Projektil deaktiviert und dem Spieler
         * ein Leben abgezogen.
         */
        private void enemyProjectileCollision()
        {
            for (int i = 0; i < this.enemies.enemies.Count; i++)
            {
                for (int j = 0; j < this.enemies.enemies[i].Weapon.projectiles.Count; j++)
                {
                    if (this.enemies.enemies[i].Weapon.projectiles[j].image.Bounds.IntersectsWith(
                        this.player.Image.Bounds))
                    {
                        this.enemies.enemies[i].Weapon.projectiles[j].disable = true;
                        this.player.subLife();
                    }
                }
            }
        }

        /**
         * Updated die schwierigkeit des Spiels. Alle 20 kills wird die schwierigkeit um 1
         * angehoben.
         */
        private void updateDifficulty()
        {
            if (this.score % 20 == 0)
            {
                difficulty++;
            }
        }

        /**
         * GAME LOOP
         * Diese Funktion wird mithilfe des Forms Element Timer in einem Interval
         * aufgerufen, um unser Spielstatus upzudaten.
         * @param space - Information ob die Leertaste gedrückt wird, um zu schießen.
         * @param left - Information ob die linke Pfeiltaste gedrückt wird, um Spieler nach links zu bewegen.
         * @param right - Information ob die linke Pfeiltaste gedrückt wird, um Spieler nach rechts zu bewegen.
         * @param pause - Information ob die P gedrückt wird, um das Spiel zu pausieren.
         * @out Spielstatus - 0:Spiel läuft oder ist Pausiert, 1:Spiel ist vorbei.
         */
        public int updateInterval(bool space, bool left, bool right, bool pause, bool mute)
        {

            this.pause = pause;

            if (!mute)
            {
                this.soundActive = true;
            }
            else
            {
                this.soundActive = false;
            }

            if (!gameOver && !this.pause)
            {

                //UserInputs
                //left, Player nach links bewegen
                if (left)
                {
                    if (this.player.Position.X - this.player.Speed >= 0)
                    {
                        this.player.Position.incX(-1 * this.player.Speed);
                        this.player.syncPlayerImage();
                    }
                }
                //right, Player nach rechts beweegen
                if (right)
                {
                    if (this.player.Position.X + this.player.Speed <= this.gameField.Width)
                    {
                        this.player.Position.incX(this.player.Speed);
                        this.player.syncPlayerImage();
                    }
                }

                //Space, Player schießt
                if (space)
                {
                    if (this.player.Inventory.weapons[this.player.CurrentWeapon].isReadyToShoot())
                    {
                        this.player.shoot();

                        HelperLib.playSound(soundActive, Properties.Resources.Shoot1);
                        
                        this.player.Inventory.weapons[this.player.CurrentWeapon].setCooldown();
                    }
                }

                //Cooldown der Spielerwaffe aktualisieren.
                this.player.Inventory.weapons[this.player.CurrentWeapon].tickCooldown();

                //Projektile des Spielers bewegen
                this.player.updateProjectiles(20);

                //Gegner bewegen
                this.enemies.updateEnemies(2, gameField);

                //Spawnen von Gegnern
                if (spawnTimer == 20)
                {
                    this.enemies.spawnEnemy(gameField, difficulty);
                    spawnTimer = 0;
                }

                //Gegner Greifen an
                enemyShootManagement();


                //Collisionsüberprüfung
                //ob Projectile von Gegnerwaffe mit Spieler intersected
                enemyProjectileCollision();
                //ob Projectile von Spielerwaffe mit Gegner intersected
                checkProjectileCollision();
                //ob Gegner in den Spieler rein fliegt
                checkPlayerCollision();


                //Entfernen von Objekten, die verschwinden sollen
                clearDisabled();

                //Updater für die Explosionsanimation
                this.enemies.updateAnimations();

                //Prüfen ob das Spiel vorbei ist
                checkGameOver();

                spawnTimer++;
                return 0;
            }
            else
            {
                if (!gameOver)
                {
                    return 0;
                }
                return 1;
            }
        }
    }
}
