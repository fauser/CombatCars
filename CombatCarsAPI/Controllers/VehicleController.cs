using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using CombatCarsAPI.Models;
using CombatCarsAPI.Security;

namespace CombatCarsAPI.Controllers
{
    [TokenValidationAttribute]
    public class VehicleController : ApiController
    {
        // GET api/vehicle
        public IEnumerable<Vehicle> Get()
        {
            using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
            {
                Token token = TokenHandler.GetTokenSpecifiedInRequest(repository, Request);

                if (token == null)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                var vehicles = from v in repository.Vehicles
                               where v.UserVehicles.FirstOrDefault().UserId == token.UserId
                               select v;

                TokenHandler.SetNewLeaseTime(repository, token);

                return vehicles.ToList();
            }
        }

        // GET api/vehicle/5
        public Vehicle Get(int id)
        {
            using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
            {
                Token token = TokenHandler.GetTokenSpecifiedInRequest(repository, Request);
                if (token == null)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                var vehicle = (from v in repository.Vehicles
                               where
                                v.UserVehicles.FirstOrDefault().UserId == token.UserId
                                && v.VehicleId == id
                               select v);

                TokenHandler.SetNewLeaseTime(repository, token);

                if (vehicle == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return vehicle.FirstOrDefault();
            }
        }

        // POST api/vehicle
        public void Post([FromBody]Vehicle value)
        {
            using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
            {
                Token token = TokenHandler.GetTokenSpecifiedInRequest(repository, Request);
                if (token == null)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                if (value == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                repository.Vehicles.InsertOnSubmit(value);
                repository.SubmitChanges();

                UserVehicle uv = new UserVehicle() { UserId = token.UserId, VehicleId = value.VehicleId };

                repository.UserVehicles.InsertOnSubmit(uv);
                repository.SubmitChanges();

                TokenHandler.SetNewLeaseTime(repository, token);
            }
        }

        // PUT api/vehicle/5
        public void Put(int id, [FromBody]Vehicle value)
        {
            using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
            {
                Token token = TokenHandler.GetTokenSpecifiedInRequest(repository, Request);
                if (token == null)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                if (value == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                var vehicle = (from v in repository.Vehicles
                               where
                                v.UserVehicles.FirstOrDefault().UserId == token.UserId
                                && v.VehicleId == id
                               select v).FirstOrDefault();

                if (vehicle != null)
                {
                    foreach (PropertyInfo p in typeof(Vehicle).GetProperties())
                    {
                        if (p.Name != "VehicleId" && p.Name != "UserVehicles")
                        {
                            object reflectedValue = p.GetValue(value, new object[] { });
                            p.SetValue(vehicle, reflectedValue, new object[] { });
                        }
                    }
                    repository.SubmitChanges();
                }

                TokenHandler.SetNewLeaseTime(repository, token);
            }
        }

        // DELETE api/vehicle/5
        public void Delete(int id)
        {
            using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
            {
                Token token = TokenHandler.GetTokenSpecifiedInRequest(repository, Request);
                if (token == null)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                Vehicle vehicle = (from v in repository.Vehicles
                                   where
                                    v.UserVehicles.FirstOrDefault().UserId == token.UserId
                                    && v.VehicleId == id
                                   select v).FirstOrDefault();

                if (vehicle == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                if (vehicle.UserVehicles != null)
                    repository.UserVehicles.DeleteOnSubmit(vehicle.UserVehicles.FirstOrDefault());

                repository.Vehicles.DeleteOnSubmit(vehicle);
                repository.SubmitChanges();

                TokenHandler.SetNewLeaseTime(repository, token);
            }
        }

    }
}
