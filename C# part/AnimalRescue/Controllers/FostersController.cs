using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AnimalRescue.Models;


namespace AnimalRescue.Controllers
{
    public class FostersController : ApiController
    {

        private FostersDB _fostersDB = new FostersDB();

        //GET
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_fostersDB.GetAllFosters());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Fosters!\n  -- " + ex.Message);
            }
        }


        //GET BY USER ID PUBLISHER 
        public IHttpActionResult Get(int id_user)
        {
            try
            {
                return Ok(_fostersDB.GetAllFostersByUser(id_user));
            }
            catch (Exception ex)
            {
                return BadRequest($"could not get all the Fosters of the id publish {id_user}!\n  -- " + ex.Message);
            }
        }



        //GET BY CITY
        public IHttpActionResult Get(string city)
        {
            try
            {
                return Ok(_fostersDB.GetFostersByCity(city));
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Fosters with the city name '{city}' " + ex.Message);
            }
        }







        // POST CREATE NEW FOSTER
        public IHttpActionResult Post([FromBody] Fosters Foster2Insert)
        {

            try
            {
                int res = _fostersDB.InsertFosterToDb(Foster2Insert);
                if (res == -1)
                {
                    return Content(HttpStatusCode.BadRequest, $"Foster id_foster = {Foster2Insert.ID_Foster} was not created in the DB!!!");
                }
                Foster2Insert.ID_Foster = res;
                //return Created(new Uri(Url.Link("GetFosterByID", new { id = res })), Foster2Insert);
                return Created(new Uri(Request.RequestUri.AbsoluteUri + res), Foster2Insert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //UPDATE - PUT (update foster post)
        public IHttpActionResult Put(Fosters Foster2Update)
        {
            try
            {
                Fosters f = _fostersDB.GetFostersByID(Foster2Update.ID_Foster);
                if (f != null)
                {
                    int res = _fostersDB.UpdateFosters(Foster2Update);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.NotModified, $"Foster with id {Foster2Update.ID_Foster} exsits but could not be modified!!!");
                }
                return Content(HttpStatusCode.NotFound, "Foster with id = " + Foster2Update.ID_Foster + " was not found to update!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }



        //DELETE FOSTER POST BY ID_FOSTER
        public IHttpActionResult Delete(int id_foster)
        {
            try
            {
                Fosters f = _fostersDB.GetFostersByID(id_foster);
                if (f != null)
                {
                    int res = _fostersDB.DeleteFosterByID(id_foster);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, $"Foster with id_foster {id_foster} exsits but could not be deleted!!!");
                }
                return Content(HttpStatusCode.NotFound, "Foster with id_foster = " + id_foster + " was not found to delete!!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
