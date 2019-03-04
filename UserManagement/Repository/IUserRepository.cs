using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models;

namespace UserManagement.Repository
{
    public interface IUserRepository
    {
        List<Users> GetAllUser();
        List<Users> GetUserById(string id);
        List<Users> GetRole( );

        string LoginUser(string un, string pw);

        string CreateUser(string un, string pw);

        string ChangePassword(string UserName, string OldPassword, string NewPassword, string ConfirmPassword);
    }
}