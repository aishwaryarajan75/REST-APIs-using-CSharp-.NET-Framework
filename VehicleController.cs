using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MitchellCodingChallenge.Models;

namespace MitchellCodingChallenge.Controllers
{
    public class VehicleController : ApiController
    {
        VehiclePersistenceInterface vp ;
        public VehicleController()
        {
            vp = new VehiclePersistence();
        }

        public VehicleController(VehiclePersistenceInterface v)
        {
            vp = v;
        }


        // GET: api/Vehicle
        public List<Vehicle> Get()
        {
            return vp.getAllVehicles();
        }

        // GET: api/Vehicle/5
        public Vehicle Get(int id)
        {
            Vehicle vehicle = vp.getVehicle(id);
            return vehicle;    
        }

        // POST: api/Vehicle
        public HttpResponseMessage  Post([FromBody] Vehicle vehicle)
        {
            int id;
            id = vp.saveVehicle(vehicle);
            vehicle.Id = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("vehicle/{0}", id));
            return response;
        }

        // PUT: api/Vehicle/5
        public HttpResponseMessage Put(int id, [FromBody]Vehicle v)
        {
            bool recordexisted = false;
            recordexisted = vp.updateVehicle(id, v);
            HttpResponseMessage response;
            if (recordexisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        // DELETE: api/Vehicle/5
        public HttpResponseMessage Delete(int id)
        {
            bool recordexisted = false;
            recordexisted = vp.deleteVehicle(id);
            HttpResponseMessage response;
            if (recordexisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
