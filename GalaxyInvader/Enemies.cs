using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace GalaxyInvader
{

    /*
     * Klasse Enemies definiert eine Liste von allen Gegnern auf dem Spielfeld
     */
    public class Enemies
    {
        //Gegner
        public List<Enemy> enemies;


        //Liste von Explosions Animationen. Diese wird hier gespeichert, da beim tod eines Gegners,
        //an der Position die Animation abgespielt werden soll.
        List<ExplodeAnimation> ex = new List<ExplodeAnimation>();


        Random rdm = new Random();

        /**
         * Konstruktor for das Gegner Listen Object
         */
        public Enemies()
        {
            this.enemies = new List<Enemy>();
        }


        /**
         * Setzt einen neuen Gegner auf das Spielfeld. X Position wird zufällig aus vorgegebenen X Spawnpositionen ausgewählt.
         * Außerdem wird in isEqual geprüft, dass der Y abstand zu anderen Gegnern groß genug ist um Überschneidungen zu vermeiden.
         * @param parent - Parent Element auf das der neue Gegner gesetzt werden soll (In dem Fall das Spielfeld)
         * @param strength - Definiert die Stärke der Gegner. In dem falle werden die Lebenspunkte des Gegners bestimmt.
         */
        public void spawnEnemy(PictureBox parent, int strength)
        {
            bool spawnFree = true;

            int[] tx = HelperLib.spawnHelperArrayX();

            Position pos = new Position(tx[rdm.Next(0, tx.Length)], -50);

            //Falls noch kein Gegner vorhanden
            if (enemies.Count == 0)
            {
                this.enemies.Add(new Enemy(
                            HelperLib.createEnemy(0, parent),
                            pos,
                            strength
                            ));
            }
            else
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (pos.isEqual(enemies[i].Position))
                    {
                        spawnFree = false;
                    }
                }
                if (spawnFree)
                {
                    this.enemies.Add(new Enemy(
                            HelperLib.createEnemy(0, parent),
                            pos,
                            strength
                            ));
                }
            }
        }

        /**
         * Löscht alle Projektile des Gegners. Falls ein Gegner stirbt, sollten seine Projektile verschwinden.
         * @param index - Index Position des Gegners, dessen Projektile gelöscht werden sollen.
         */
        private void clearAllProjectiles(int index)
        {
            for (int i = 0; i < this.enemies[index].Weapon.projectiles.Count; i++)
            {
                this.enemies[index].Weapon.projectiles[i].disposeProjectile();
            }
        }

        /**
         * Entfernen eines Gegners an Position, wenn ein Gegner stirbt.
         * @param x - Index Position des Gegners, der entfernt werden soll.
         * @param parent - Parent Element (Spielfeld) . Wird hier verwendet um die explosions Animation zu setzen. 
         */
        public void killEnemyAt(int x, PictureBox parent)
        {
            this.ex.Add(new ExplodeAnimation(HelperLib.createExplodeAnimation(this.enemies[x].Position, parent)));

            clearAllProjectiles(x);
            this.enemies[x].disposeEnemy();
            this.enemies.RemoveAt(x);
        }

        /**
         * Bewegt die Gegner über das Spielfeld in Y Richtung.
         * @param offset - gibt die Geschwindigzeit bzw. die Schrittweite eines Bewegungsintervals an.
         * @param parent - Parent Element. Wird benötigt um zu schauen, ob der Gegner die Map überschreitet.
         */
        public void updateEnemies(int offset, PictureBox parent)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Position.Y += offset;
                enemies[i].syncEnemy();
                int yBound = enemies[i].Position.Y;
                if (yBound > parent.Height)
                {
                    enemies[i].disposeEnemy();
                    this.enemies.RemoveAt(i);
                }
            }
        }

        /**
         * Verwaltet die Explosions Animationen. Zählt die Animation Timer weiter bis sie 0 erreichen.
         * Löscht anschließend bei Animationsende die Explosionen.
         */
        public void updateAnimations()
        {
            for (int i = ex.Count - 1; i >= 0; i--)
            {
                ex[i].tickAnimation();
                if (ex[i].getTimer() <= 0)
                {
                    ex[i].image.Dispose();
                    ex.RemoveAt(i);
                }
            }
        }
    }
}
