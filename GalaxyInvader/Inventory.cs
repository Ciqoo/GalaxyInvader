using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Inventory stellt das Inventar eines Spielers da,
     * Falls der Spieler in der lage ist, mehrere Waffen zu haben.
     */
    public class Inventory
    {
        public Weapon[] weapons;

        /**
         * Konstruktor für ein Inventar.
         */
        public Inventory()
        {
            this.weapons = new Weapon[4];
            for (int i = 0; i < this.weapons.Length; i++)
            {
                this.weapons[i] = new Weapon(8);
            }
        }
    }
}
