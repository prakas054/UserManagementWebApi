using System.Collections.Generic;
using System.Web.Http;
using UserManagement.Repository;
using UserManagement.Models;
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;
using System;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace UserManagement.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {       
        IUserRepository iuserRepository;
        public UsersController(IUserRepository _IUserRepository)
        {
            iuserRepository = _IUserRepository;

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<Users> ListOfUser = iuserRepository.GetAllUser();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ListOfUser));
            }
            catch (SqlException e)
            {
                var message = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message)
                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpResponseException(message)));
                
            }
        }       

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody]JObject data)
        {

            Users user = new Users();
            Membership membership = new Membership();

            user._UserName = (string)data["Username"];
            membership._Password = (string)data["Password"];

            if (user._UserName == null || membership._Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("User name or Password should not be Empty"))
                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpResponseException(message)));
            }
            else
            {
                string CreateUserReturnValue = iuserRepository.Create(user._UserName, membership._Password);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, CreateUserReturnValue));
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody] JObject data)
        {
            Users user = new Users();
            Membership membership = new Membership();

            user._UserName = (string)data["Username"];
            membership._Password = (string)data["CurrentPassword"];
            membership._Password = (string)data["NewPassword"];
            if (user._UserName == null || membership._Password == null || membership._Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("User name or Password should not be Empty"))
                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpResponseException(message)));
            }
            else
            {
                string ChangePasswordReturnValue = iuserRepository.ChangePassword(user._UserName, membership._Password, membership._Password);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ChangePasswordReturnValue));
            }
        }

    }
    
    
}

