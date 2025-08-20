using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Klasse stellt die Positionen der Spiel Objekte dar.
     */
    public class Position
    {
        private int x;
        private int y;

        //Getter - Setter
        public int X { get { return this.x; } set { this.x = value; } }
        public int Y { get { return this.y; } set { this.y = value; } }

        /**
         * Konstruktor der Position.
         * @param x - X Koordinate.
         * @param y - Y Koordinate.
         */
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /**
         * Konstruktor der Position x:0 y:0.
         */
        public Position()
        {
            this.x = 0;
            this.y = 0;
        }

        /**
         * Erhöt die X Position um einen wert.
         * @param v - wert der auf die X Position addiert wird.
         */
        public void incX(int v)
        {
            this.x += v;
        }

        /**
         * Erhöt die Y Position um einen wert.
         * @param v - wert der auf die Y Position addiert wird.
         */
        public void incY(int v)
        {
            this.y += v;
        }

        /**
         * Vergleich die Position der Instanz mit einer gegeben Position auf X Positonen.
         * Anschließend wird geprüft, ob ein Definierter Y Abstand eingehalten wird.
         * (Wird bei den Spawn Positionen der Gegner verwendet, sodass diese mit
         * genügend abstand spawnen um überschneidungen zu vermeiden.)
         * @param pos - Position mit der verglichen werden soll.
         * @out true - false.
         */
        public bool isEqual(Position pos)
        {
            bool eq = true;
            if (this.x == pos.x)
            {
                if (Math.Abs(this.y - pos.y) >= 110)
                {
                    eq = false;
                }
                else
                {
                    eq = true;
                }
            }
            else
            {
                eq = false;
            }
            return eq;
        }

        /**
         * Operator == um Positionen auf gleichheit zu überprüfen
         * @param p1 - Position 1 zum vergleichen.
         * @param p2 - Position 2 zum vergleichen.
         * @out true - false.
         */
        public static bool operator ==(Position p1, Position p2) => p1.x == p2.x && p1.y == p2.y;

        /**
         * Operator != um Positionen auf ungleichheit zu überprüfen
         * @param p1 - Position 1 zum vergleichen.
         * @param p2 - Position 2 zum vergleichen.
         * @out true - false.
         */
        public static bool operator !=(Position p1, Position p2) => p1.x != p2.x || p1.y != p2.y;

        /**
         * ToString() methode wird überschrieben um diese Position als String auszugeben.
         * @out Position in string Format.
         */
        public override string ToString()
        {
            return $"X:{this.x}  Y:{this.y}\n";
        }
    }
}
