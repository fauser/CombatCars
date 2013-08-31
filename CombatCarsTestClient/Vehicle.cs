using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CombatCarsTestClient
{
    public class Vehicle
    {
        //[{"VehicleId":1,"Name":"Maximum Overkill","MaximumSpeed":5}]

        public Vehicle()
        {

        }

        public int VehicleId { get; set; }
        public string Name { get; set; }
        public int MaximumSpeed { get; set; }


        public override string ToString()
        {
            return string.Format("VehicleId:{0}, Name:{1}, MaximumSpeed:{2}", VehicleId, Name, MaximumSpeed);
        }
    }
}
