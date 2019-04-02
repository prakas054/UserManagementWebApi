using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagement.Controllers
{
    public class LoginController : ApiController
    {
        IUserRepository<Users> iuserRepository;
        public LoginController(IUserRepository<Users> _IUserRepository)
        {
            iuserRepository = _IUserRepository;

        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] Users userObj)
        {

            var UserName = userObj._UserName;
            var Password = userObj._Password;

            if (UserName == null || Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("User name or Password not found"))

                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpResponseException(message)));
            }
            else
            {
                string UserNameReturnValue = iuserRepository.Login(userObj);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserNameReturnValue));
            }
        }
    }
}
