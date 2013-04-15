using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CombatCarsAPI.Models
{
    internal class ConnectionStringHandler
    {
        internal static string ConnectionString()
        {
            return @"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234";
        }
    }
}