using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericGameEngine;

namespace CombatCarsWinformsClient
{
    class EnemyDef
    {
        public EnumEnemyType EnemyType { get; set; }
        public double LaunchTime { get; set; }

        public EnemyDef()
        {
            EnemyType = EnumEnemyType.CannonFodder;
            LaunchTime = 0;
        }

        public EnemyDef(EnumEnemyType enemyType, double launchTime)
        {
            EnemyType = enemyType;
            LaunchTime = launchTime;
        }
    }
}
