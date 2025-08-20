using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyInvader
{
    /*
     * Interface für ein sterbliches Object.
     */
    public interface IKillable
    {
        //Object ist gestorben.
        bool isKilled();
        //Object erhält ein Leben.
        void addLife();
        //Object verliert ein Leben.
        void subLife();
        //Gibt Lebensanzahl des Objects zurück.
        int getLife();
    }
}
