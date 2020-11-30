using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AnimalRescue.Models;


namespace AnimalRescue.Controllers
{
    public class RescuersController : ApiController
    {

        private RescuersDB _rescDB = new RescuersDB();

        //[EnableCors( "*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_rescDB.GetAllRescuers());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Rescuers!\n  -- " + ex.Message);
            }
        }
    }
}
