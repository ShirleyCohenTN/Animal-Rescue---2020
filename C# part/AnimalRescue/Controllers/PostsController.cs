using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;


using AnimalRescue.Models;


namespace AnimalRescue.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PostsController : ApiController
    {
        private PostsDB _postsDB = new PostsDB();
        private UsersDB _usersDB = new UsersDB();

        //GET
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_postsDB.GetAllPosts());
            }
            catch (TypeInitializationException ex)
            {
                return BadRequest("could not get all the Posts!\n  -- " + ex.InnerException);
            }
        }



        //GET BY USER ID PUBLISHER 
        public IHttpActionResult Get(int id_user)
        {
            try
            {
                return Ok(_postsDB.GetAllPostsByUser(id_user));
            }
            catch (Exception ex)
            {
                return BadRequest($"could not get all the Posts of the id publish {id_user}!\n  -- " + ex.Message);
            }
        }



        //GET BY CITY
        public IHttpActionResult Get(string city)
        {
            try
            {
                return Ok(_postsDB.GetPostsByCity(city));
            }
            catch (Exception ex)
            {
                return BadRequest("could not get all the Posts with the city name '{city}' " + ex.Message);
            }
        }



        // POST CREATE NEW ANIMAL RESCUE POST
        [Route("api/uploadpost")]
        public IHttpActionResult Post([FromBody] Posts Post2Insert)
        {

            try
            {
                int res = _postsDB.InsertPostToDb(Post2Insert);
                if (res == -1)
                {
                    return Content(HttpStatusCode.BadRequest, $"Post id_post = {Post2Insert.ID_Post} was not created in the DB!!!");
                }

                Post2Insert.ID_Post = res;

                List<Users> allUsers = _usersDB.GetAllUsers();
                if (allUsers.Count > 0)
                {
                    List<Users> users = allUsers.FindAll(user => PushNotification.GetDistance((double)Post2Insert.Longitude, (double)Post2Insert.Latitude, (double)user.Lng, (double)user.Lat) <= 15000);

                    foreach (Users u in users)
                    {
                        PushData pnd = new PushData(u.Token, "התראת חיה במצוקה", Post2Insert.Animal_Case);
                        PushNotification.push(pnd);
                    }
                }

                //return Created(new Uri(Url.Link("GetFosterByID", new { id = res })), Foster2Insert);
                return Created(new Uri(Request.RequestUri.AbsoluteUri + res), Post2Insert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //UPDATE - PUT (update post - status+date)
        public IHttpActionResult Put(Posts Post2Update)
        {
            try
            {
                Posts p = _postsDB.GetPostByID(Post2Update.ID_Post);
                if (p != null)
                {
                    int res = _postsDB.UpdatePost(Post2Update);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.NotModified, $"Post with id {Post2Update.ID_Post} exsits but could not be modified!!!");
                }
                return Content(HttpStatusCode.NotFound, "Post with id = " + Post2Update.ID_Post + " was not found to update!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }





        //DELETE POST POST BY ID_POST
        public IHttpActionResult Delete(int id_post)
        {
            try
            {
                Posts p = _postsDB.GetPostByID(id_post);
                if (p != null)
                {
                    int res = _postsDB.DeletePostByID(id_post);
                    if (res == 1)
                    {
                        return Ok();
                    }
                    return Content(HttpStatusCode.BadRequest, $"Post with id_post {id_post} exsits but could not be deleted!!!");
                }
                return Content(HttpStatusCode.NotFound, "Post with id_post = " + id_post + " was not found to delete!!!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("uploadpicture2")]
        [HttpPost]
        public IHttpActionResult Image([FromBody] ImageFromUser img)
        {
            try
            {
                string fullpath = $"{System.Web.HttpContext.Current.Server.MapPath(".")}/{img.path}/";
                System.IO.Directory.CreateDirectory(fullpath);
                string filePath = $"{fullpath}/{img.name}.jpg";
                System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(img.base64string));
                return Ok($"http://shirley.up2app.co.il/{img.path}/{img.name}.jpg");
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }



        [Route("uploadpicture")]
        public Task<HttpResponseMessage> Post()
        {
            string outputForNir = "start---";
            List<string> savedFilePath = new List<string>();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string rootPath = HttpContext.Current.Server.MapPath("~/uploadFiles");
            var provider = new MultipartFileStreamProvider(rootPath);
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData item in provider.FileData)
                    {
                        try
                        {
                            outputForNir += " ---here";
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            outputForNir += " ---here2=" + name;

                            //need the guid because in react native in order to refresh an image it has to have a new name
                            string newFileName = Path.GetFileNameWithoutExtension(name) + "_" + CreateDateTimeWithValidChars() + Path.GetExtension(name);
                            //string newFileName = Path.GetFileNameWithoutExtension(name) + "_" + Guid.NewGuid() + Path.GetExtension(name);
                            //string newFileName = name + "" + Guid.NewGuid();
                            outputForNir += " ---here3" + newFileName;

                            //delete all files begining with the same name
                            string[] names = Directory.GetFiles(rootPath);
                            foreach (var fileName in names)
                            {
                                if (Path.GetFileNameWithoutExtension(fileName).IndexOf(Path.GetFileNameWithoutExtension(name)) != -1)
                                {
                                    File.Delete(fileName);
                                }
                            }

                            //File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));
                            File.Copy(item.LocalFileName, Path.Combine(rootPath, newFileName), true);
                            File.Delete(item.LocalFileName);
                            outputForNir += " ---here4";

                            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            outputForNir += " ---here5";
                            string fileRelativePath = "~/uploadFiles/" + newFileName;
                            outputForNir += " ---here6 imageName=" + fileRelativePath;
                            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                            outputForNir += " ---here7" + fileFullPath.ToString();
                            savedFilePath.Add(fileFullPath.ToString());
                        }
                        catch (Exception ex)
                        {
                            outputForNir += " ---excption=" + ex.Message;
                            string message = ex.Message;
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, "nirchen " + savedFilePath[0] + "!" + provider.FileData.Count + "!" + outputForNir + ":)");
                });
            return task;
        }

        private string CreateDateTimeWithValidChars()
        {
            return DateTime.Now.ToString().Replace('/', '_').Replace(':', '-').Replace(' ', '_');
        }


    }
}
