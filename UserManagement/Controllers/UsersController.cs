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
        IUserRepository<Users> iuserRepository;
        ISearchUser isearchUser;
        private IUserRepository<Users> _ISearchUser;

        public UsersController(IUserRepository<Users> _IUserRepository, ISearchUser _ISearchUser)
        {
            iuserRepository = _IUserRepository;
            isearchUser = _ISearchUser;

        }

        public UsersController(IUserRepository<Users> searchUser)
        {
            _ISearchUser = searchUser;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<Users> ListOfUser = isearchUser.GetAllUser();
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
        public IHttpActionResult Create([FromBody] Users userObj)
        {
            var UserName = userObj._UserName;
            var Password = userObj._Password;

            if (UserName == null || Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("User name or Password should not be Empty"))
                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpResponseException(message)));
            }
            else
            {
                string CreateUserReturnValue = iuserRepository.Create(userObj);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, CreateUserReturnValue));
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody] Users userObj)
        {
            var UserName = userObj._UserName;
            var CurrentPassword = userObj._Password;
            var NewPassword = userObj._Password;

            if (UserName == null || CurrentPassword == null || NewPassword == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("User name or Password should not be Empty"))
                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpResponseException(message)));
            }
            else
            {
                string ChangePasswordReturnValue = iuserRepository.ChangePassword(userObj);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ChangePasswordReturnValue));
            }
        }

    }
    
    
}

