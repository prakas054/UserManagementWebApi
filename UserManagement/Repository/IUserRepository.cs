using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models;

namespace UserManagement.Repository
{
    public interface ISearchUser
    {
        IEnumerable<Users> GetAllUser();
        IEnumerable<Users> GetRole();
    }

    public interface IUserRepository<T1> where T1 : class
    {   
        
        string Login(T1 t);
        string Create(T1 t);
        string ChangePassword(T1 t);

    }
}