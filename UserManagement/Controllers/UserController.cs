using System.Collections.Generic;
using System.Web.Http;
using UserManagement.Repository;
using UserManagement.Models;
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;

namespace UserManagement.Controllers
{
    [RoutePrefix("api/UserController")]
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
            List<Users> ListOfUser = new List<Users>();
            var all = iuserRepository.GetAllUser();

            foreach (var x in all)
            {
                Users UsersObj = new Users();
               // Membership MembershipObj = new Membership();
                UsersObj._UserName = x._UserName;
                //MembershipObj._Password = x._Password;
                ListOfUser.Add(UsersObj);
            }
            var response = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ListOfUser));
            return response;
        }

        [HttpGet]
        [Route("LoginUser/{sun}/{spw}")]
        public IHttpActionResult LoginUser(string sun, string spw)
        {
            string UN = sun;
            string PW = spw;
            string UserNameReturnValue = iuserRepository.LoginUser(UN, PW);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, UserNameReturnValue));
        }

        [HttpPost]
        [Route("CreateUser/{sun}/{spw}")]
        public IHttpActionResult CreateUser(string sun, string spw)

        {
            string CreateUserReturnValue = iuserRepository.CreateUser(sun, spw);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, CreateUserReturnValue));
        }

        [HttpPut]
        [Route("ChangePassword/{UserName}/{OldPassword}/{NewPassword}")]

        public IHttpActionResult ChangePassword(string UserName, string OldPassword, string NewPassword)
        {

            string ChangePasswordReturnValue = iuserRepository.ChangePassword(UserName, OldPassword, NewPassword);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ChangePasswordReturnValue));
        }

    }
    
    
}

