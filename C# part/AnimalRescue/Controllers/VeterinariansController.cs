using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AnimalRescue.Models;


namespace AnimalRescue.Controllers
{
    public class VeterinariansController : ApiController
    {
        private VeterinariansDB _vetDB = new VeterinariansDB();

        //[EnableCors( "*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_vetDB.GetAllVeterinarians());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Veterinarians!\n  -- " + ex.Message);
            }
        }


        //GET BY CITY NAME
        public IHttpActionResult Get(string city)
        {
            try
            {
                return Ok(_vetDB.GetVeterinariansByCity( city));
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Veterinarians with the city name '{city}' !\n  -- " + ex.Message);
            }
        }
    }
}
