using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Weapon stellt eine Waffe dar, welche Projektile enthält,
     * die von der Waffe abgefeuert wurden.
     */
    public class Weapon
    {
        //Feuerrate die der cooldown erreichen muss, sodass
        //die Waffe wieder schießen kann.
        int firerate;
        public int cooldown;
        public List<Projectile> projectiles;

        /**
         * Konstruktor einer Waffe.
         * @param firerate - Feuerrate der Waffe.
         */
        public Weapon(int firerate)
        {
            this.firerate = firerate;
            this.cooldown = 0;
            this.projectiles = new List<Projectile>();
        }

        /**
         * Setzt die Waffe auf Cooldown.
         * Wird nach dem Abfeuern der waffe ausgeführt.
         */
        public void setCooldown()
        {
            this.cooldown = firerate;
        }

        /**
         * Zählt den Cooldown über den GameLoop Interval herunter.
         * Wird im GameLoop bzw. im updateInterval() in der Klasse
         * Game aufgerufen.
         */
        public void tickCooldown()
        {
            if (this.cooldown > 0)
            {
                this.cooldown--;
            }
        }

        /**
         * Gibt zurück, ob die Waffe schießen kann bzw. ob der
         * Cooldown der Waffe abgelaufen ist.
         * @out true - false.
         */
        public bool isReadyToShoot() => this.cooldown == 0;

    }
}
