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
        private CombatCarsAPIModelDataContext db = new CombatCarsAPIModelDataContext(@"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234");

        // GET api/vehicle
        public IEnumerable<Vehicle> Get()
        {
            var vehicles = from v in db.Vehicles
                           select v;

            return vehicles;
        }

        // GET api/vehicle/5
        public Vehicle Get(int id)
        {
            var vehicle = (from v in db.Vehicles
                           where v.VehicleId == id
                           select v).FirstOrDefault();

            if (vehicle == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return vehicle;
        }

        // POST api/vehicle
        public void Post([FromBody]Vehicle value)
        {
        }

        // PUT api/vehicle/5
        public void Put(int id, [FromBody]Vehicle value)
        {
        }

        // DELETE api/vehicle/5
        public void Delete(int id)
        {
        }
    }
}
