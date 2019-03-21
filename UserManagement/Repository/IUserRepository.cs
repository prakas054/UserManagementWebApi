using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models;

namespace UserManagement.Repository
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUser();
        IEnumerable<Users> GetRole( );
        string Login(string un, string pw);
        string Create(string un, string pw);
        string ChangePassword(string UserName, string OldPassword, string NewPassword);

    }
}