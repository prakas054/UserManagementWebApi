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
    public class UserController : ApiController
    {

        IUserRepository iuserRepository;


        public UserController(IUserRepository _IUserRepository)
        {
            iuserRepository = _IUserRepository;

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                List<Users> ListOfUser = iuserRepository.GetAllUser();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ListOfUser));
            }
            catch (SqlException e)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(e.Message)
                };
                throw new HttpResponseException(message);
            }
        }

        [HttpGet]
        [Route("Login")]
        public IHttpActionResult Login(string sun, string spw)
        {
            string UserNameReturnValue = iuserRepository.Login(sun, spw);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserNameReturnValue));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(string sun, string spw)

        {
            string CreateUserReturnValue = iuserRepository.Create(sun, spw);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, CreateUserReturnValue));
        }

        [HttpPut]

        public IHttpActionResult ChangePassword(string UserName, string OldPassword, string NewPassword)
        {

            string ChangePasswordReturnValue = iuserRepository.ChangePassword(UserName, OldPassword, NewPassword);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ChangePasswordReturnValue));
        }

    }
    
    
}

