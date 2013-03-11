using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CombatCarsRestAPI.Models.Vehicle;
using CombatCarsRestAPI.Models.Vehicle.Frame.WheelFrame;
using CombatCarsRestAPI.Services;

namespace CombatCarsRestAPI.Controllers
{
    public class VehicleController : ApiController
    {
        private VehicleRepository carRepository;

        public VehicleController()
        {
            this.carRepository = new VehicleRepository();
        }

        public Vehicle[] Get()
        {
            return this.carRepository.GetAllVehicles();
        }

        public Vehicle Get(int id)
        {
            return this.carRepository.GetVehicleWithId(id);
        }

        public HttpResponseMessage Post(Vehicle vehicle)
        {
            this.carRepository.SaveCar(vehicle);
            var response = Request.CreateResponse<Vehicle>(System.Net.HttpStatusCode.Created, vehicle);
            return response;
        }
    }
}
