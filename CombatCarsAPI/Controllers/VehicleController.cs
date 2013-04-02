using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Controllers
{
    public class VehicleController : ApiController
    {
        private ComtaCarsAPIEntities db = new ComtaCarsAPIEntities();

        // GET api/Vehicle
        public dynamic GetVehicles()
        {
            return db.Vehicle.AsEnumerable();
        }

        // GET api/Vehicle/5
        public Vehicle GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return vehicle;
        }

        // PUT api/Vehicle/5
        public HttpResponseMessage PutVehicle(int id, Vehicle vehicle)
        {
            if (ModelState.IsValid && id == vehicle.VehicleId)
            {
                db.Entry(vehicle).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Vehicle
        public HttpResponseMessage PostVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicle.Add(vehicle);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, vehicle);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = vehicle.VehicleId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Vehicle/5
        public HttpResponseMessage DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicle.Find(id);
            if (vehicle == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Vehicle.Remove(vehicle);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, vehicle);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}