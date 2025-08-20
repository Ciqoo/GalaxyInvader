using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse Projectile stellt ein abgefeuertes Projektil dar.
     */
    public class Projectile
    {
        public PictureBox image;
        public Position position;
        public bool disable = false;

        //destination ist das ziehl, in welche richtung das Projectil fliegt.
        //Wird nur beim Gegner verwendet.
        public Position destination = new Position(0,0);
        
        /**
         * Konstruktor für ein abgefeuertes Projektil.
         * @param vProjectile - Variante des Projektils (optisches Bild).
         * @param pos - Position des projektils.
         * @param gameField - Parent Objekt, auf dem das Projektil gesetzt wird.
         */
        public Projectile(int vProjectile, Position pos, PictureBox gameField)
        {
            this.image = HelperLib.createProjectile(vProjectile ,gameField);
            this.position = pos;
            HelperLib.convertPositionToImageLocation(this.image, this.position);
        }

        /**
         * Synchronisiert die Position des Projektils mit dem Bild des Projektils zur Animation.
         */
        public void syncProjectile()
        {
            HelperLib.convertPositionToImageLocation(this.image, this.position);
        }

        /**
         * Entfernt die Bildinformation des Projektils, wodurch es nicht mehr dargestellt wird.
         */
        public void disposeProjectile()
        {
            this.image.Dispose();
        }

        /**
         * Setzt die Zielrichtung des Projektils.
         * @param pos - Position des Ziels.
         */
        public void setDestination(Position pos)
        {
            this.destination = pos;
        }
    }
}
