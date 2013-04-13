using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Controllers
{
    public class VehicleController : ApiController
    {
        private CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(@"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234");

        // GET api/vehicle
        public IEnumerable<Vehicle> Get()
        {
            /*
            var vehicles = from v in repository.Vehicles
                           select new Models.Vehicle
                           {
                               VehicleId = v.VehicleId,
                               Name = v.Name
                           };
            return vehicles.ToList();
            */


            User user = (from u in repository.Users
                       where u.Username == "Daniel"
                       select u).FirstOrDefault();


            var vehicles = from v in repository.Vehicles
                           where v.UserVehicles.FirstOrDefault().UserId == user.UserId
                           select v;

            return vehicles.ToList();
        }

        // GET api/vehicle/5
        public Vehicle Get(int id)
        {
            var vehicle = (from v in repository.Vehicles
                           where v.VehicleId == id
                           select v).FirstOrDefault();

            if (vehicle == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return vehicle;
        }

        // POST api/vehicle
        public void Post([FromBody]Vehicle value)
        {
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


        }

        // PUT api/vehicle/5
        public void Put(int id, [FromBody]Vehicle value)
        {
            if (value == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE api/vehicle/5
        public void Delete(int id)
        {
        }
    }
}
