using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using AnimalRescue.Models;


namespace AnimalRescue.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class FarmsController : ApiController
    {
        private  FarmsDB _farmDB = new FarmsDB();
        //GET
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_farmDB.GetAllFarms());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Farms!\n  -- " + ex.Message);
            }
        }

    }
}
