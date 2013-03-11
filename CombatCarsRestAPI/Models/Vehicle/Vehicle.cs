using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombatCarsRestAPI.Models.Armour;
using CombatCarsRestAPI.Models.WeaponMount;

namespace CombatCarsRestAPI.Models.Vehicle
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FrontWeightCapacity { get; set; }
        public int BackWeightCapacity { get; set; }
        public int LeftWeightCapacity { get; set; }
        public int RightWeightCapacity { get; set; }

        /*
        public WeaponMountBase CenterWeaponMount { get; set; }
        public WeaponMountBase FrontWeaponMount { get; set; }
        public WeaponMountBase BackWeaponMount { get; set; }
        public WeaponMountBase LeftWeaponMount { get; set; }
        public WeaponMountBase RightWeaponMount { get; set; }

        public ArmourBase FrontArmour { get; set; }
        public ArmourBase BackArmour { get; set; }
        public ArmourBase LeftArmour { get; set; }
        public ArmourBase RightArmour { get; set; }
        */
    }
}