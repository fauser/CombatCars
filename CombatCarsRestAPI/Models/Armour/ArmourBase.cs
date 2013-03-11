using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombatCarsRestAPI.Models.Armour
{
    public abstract class ArmourBase
    {
        public int Armour { get; set; }
        public int Durability { get; set; }
        public int Weight { get; set; }

        public ArmourBase()
        {
            Durability = 10;
        }
    }
}