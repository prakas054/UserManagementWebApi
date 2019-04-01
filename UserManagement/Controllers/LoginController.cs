using Newtonsoft.Json.Linq;
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
        IUserRepository iuserRepository;
        public LoginController(IUserRepository _IUserRepository)
        {
            iuserRepository = _IUserRepository;

        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] JObject data)
        {

            Users user = new Users();
            Membership membership = new Membership();

            user._UserName = (string)data["Username"];
            membership._Password = (string)data["Password"];
            if (user._UserName == null || membership._Password == null)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("User name or Password not found"))

                };
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpResponseException(message)));
            }
            else
            {
                string UserNameReturnValue = iuserRepository.Login(user._UserName, membership._Password);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserNameReturnValue));
            }
        }
    }
}
