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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("LoginController");
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

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return ResponseMessage(Request.CreateResponse(406));
            }
            else
            {
                string UserNameReturnValue = iuserRepository.Login(userObj);
                return ResponseMessage(Request.CreateResponse(200));
            }
        }
    }
}
