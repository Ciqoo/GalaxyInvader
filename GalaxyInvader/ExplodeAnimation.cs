using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Explode Animation ist zur Darstellung einer Explosions Animation.
     */
    public class ExplodeAnimation
    {
        // Timer ist hier statisch gesetzt. Es wurde geprüft, wie viele intervalle benötigt werden,
        // sodass die Animation ein mal durchläuft. Bei der Animation waren es 13.
        int timer = 13;
        public PictureBox image;

        /**
         * Konstruktor für eine Explosions Animation.
         * @param img - PictureBox mit enthaltenem und Skaliertem Explosions-Gif
         */
        public ExplodeAnimation(PictureBox img)
        {
            this.image = img;
        }

        /**
         * Zählt den Animations timer runter.
         */
        public void tickAnimation()
        {
            timer--;
        }

        /**
         * Gibt den Wert des Timers zurück.
         * @out wert des Timers.
         */
        public int getTimer() => timer;
    }
}
