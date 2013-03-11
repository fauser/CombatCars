using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombatCarsRestAPI.Models.Weapon
{
    public abstract class WeaponBase
    {
        public int Damage { get; set; }
        public int DamageType { get; set; }

        public int Range { get; set; }
        public int RateOfFire { get; set; }
        public int ReloadTime { get; set; }
        public int Magazine { get; set; }
    }
}