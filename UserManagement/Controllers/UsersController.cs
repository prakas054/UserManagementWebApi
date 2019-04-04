using System.Collections.Generic;
using System.Web.Http;
using UserManagement.Repository;
using UserManagement.Models;
using System.Net.Http;
using System.Net;
using System;

namespace UserManagement.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("UserController");
        private IUserRepository<Users> _userRepository;
        private ISearchUser _searchUser;

        public UsersController(IUserRepository<Users> userRepository, ISearchUser searchUser)
        {
            _userRepository = userRepository;
            _searchUser = searchUser;
        }

        public IHttpActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<Users> ListOfUser = _searchUser.GetAllUser();
                return ResponseMessage(Request.CreateResponse(ListOfUser));
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(Users userObj)
        {
            var UserName = userObj._UserName;
            var Password = userObj._Password;

            if (UserName.Equals(null) || Password.Equals(null) )
            {
                return NotFound();
            }
            else
            {
                string CreateUserReturnValue = _userRepository.Create(userObj);
                return Ok();
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(Users user)
        {
            var UserName = user._UserName;
            var CurrentPassword = user._Password;
            var NewPassword = user._ChangePassword;

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword))
            {
                return NotFound();
            }
            else
            {
                string ChangePasswordReturnValue = _userRepository.ChangePassword(user);
                return Ok();
            }
        }
    }
}

