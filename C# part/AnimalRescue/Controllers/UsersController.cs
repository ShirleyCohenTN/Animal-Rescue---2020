using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using AnimalRescue.Models;

namespace AnimalRescue.Controllers
{
    public class UsersController : ApiController
    {
        private UsersDB _usersDB = new UsersDB();


        //[EnableCors( "*", "*", "*")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_usersDB.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the users!\n  -- " + ex.Message);
            }
        }


        //GET BY ID
        [Route("{id:int:min(1)}", Name = "GetUserByID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Users u = _usersDB.GetUserByID(id);
                if (u != null)
                {
                    return Ok(u);
                }
                return Content(HttpStatusCode.NotFound, $"User with id {id} was not found!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //[DisableCors]
        [HttpGet]
        public IHttpActionResult GET(string email, string password)
        {
            try
            {
                return Ok(_usersDB.GetUserByEmailAndPassword(email, password));
            }
            catch (TypeInitializationException e)
            {

                return Content(HttpStatusCode.NotFound, e.InnerException);
            }
            
        }




        [Route(Name = "GetUserByEmail")]
        public IHttpActionResult Get(string email_address)
        {
            try
            {
                Users res = _usersDB.GetUserByEmail(email_address);
                if (res == null)
                {
                    return Content(HttpStatusCode.NotFound, $"user with email= {email_address} was not found!");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST CREATE NEW USER
        public IHttpActionResult Post([FromBody] Users User2Insert)
        {

            try
            {
                int res = _usersDB.InsertUserToDb(User2Insert);
                if (res == -1)
                {
                    return Content(HttpStatusCode.BadRequest, $"Deceased id = {User2Insert.ID_User} was not created in the DB!!!");
                }
                User2Insert.ID_User = res;
                return Created(new Uri(Url.Link("GetUserByID", new { id = res })), User2Insert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


       


        //DELETE
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Users u = _usersDB.GetUserByID(id);
                if (u != null)
                {
                    int res = _usersDB.DeleteUserByID(id);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, $"User with id {id} exsits but could not be deleted!!!");
                }
                return Content(HttpStatusCode.NotFound, "User with id = " + id + " was not found to delete!!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //UPDATE - PUT (update - token + lng + lat)
        [Route("api/users/updatetoken")]
        public IHttpActionResult Put(Users User2Update)
        {
            try
            {
                Users p = _usersDB.GetUserByID(User2Update.ID_User);
                if (p != null)
                {
                    int res = _usersDB.UpdateUser(User2Update);
                    if (res == 1)
                    {
                        return Ok("OK");
                    }
                    return Content(HttpStatusCode.NotModified, $"User with id {User2Update.ID_User} exsits but could not be modified!!!");
                }
                return Content(HttpStatusCode.NotFound, "User with id = " + User2Update.ID_User + " was not found to update!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }




    }
}

