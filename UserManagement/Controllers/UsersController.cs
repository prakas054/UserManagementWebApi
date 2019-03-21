using System.Collections.Generic;
using System.Web.Http;
using UserManagement.Repository;
using UserManagement.Models;
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;
using System;
using System.Data.SqlClient;

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
                var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message)
                };
                throw new HttpResponseException(message);
            }
        }

        [HttpGet]
        [Route("Login")]
        public IHttpActionResult Login(string UserName, string Password)
        {
            if (UserName == null || Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("User name or Password not found"))

                };
                throw new HttpResponseException(message);
            }
            else
            {
                string UserNameReturnValue = iuserRepository.Login(UserName, Password);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserNameReturnValue));
            }
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(string UserName, string Password)
        {
            if (UserName == null || Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("User name or Password should not be Empty"))
                };
                throw new HttpResponseException(message);
            }
            else
            {
                string CreateUserReturnValue = iuserRepository.Create(UserName, Password);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, CreateUserReturnValue));
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(string UserName, string CurrentPassword, string NewPassword)
        {
            if (UserName == null || CurrentPassword == null || NewPassword == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("User name or Password should not be Empty"))
                };
                throw new HttpResponseException(message);
            }
            else
            {
                string ChangePasswordReturnValue = iuserRepository.ChangePassword(UserName, CurrentPassword, NewPassword);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ChangePasswordReturnValue));
            }
        }

    }
    
    
}

