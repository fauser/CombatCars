using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CombatCarsRestAPI.Models;
using CombatCarsRestAPI.Models.Armour;
using CombatCarsRestAPI.Models.Vehicle;
using CombatCarsRestAPI.Models.Vehicle.Frame.WheelFrame;

namespace CombatCarsRestAPI.Services
{
    public class VehicleRepository
    {
        private const string CacheKey = "CarStore";

        public VehicleRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    Vehicle[] cars = new Vehicle[] { new Vehicle { Id = 1, Name = "Grave Digger" }, new Vehicle { Id = 2, Name = "Maximum Overkill" } };

                    ctx.Cache[CacheKey] = cars;
                }
            }
        }

        public Vehicle[] GetAllVehicles()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Vehicle[])ctx.Cache[CacheKey];
            }

            return new Vehicle[] { new Vehicle { Id = 0, Name = "Placeholder" } };
        }

        public Vehicle GetVehicleWithId(int id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                Vehicle[] cars = (Vehicle[])ctx.Cache[CacheKey];

                var c = from car in cars
                        where car.Id == id
                        select car;

                return c.FirstOrDefault();
            }

            return new Car { Id = 0, Name = "Placeholder" };
        }

        public bool SaveCar(Vehicle car)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Vehicle[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(car);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}