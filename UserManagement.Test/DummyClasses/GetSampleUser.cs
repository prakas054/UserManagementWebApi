using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Test.DummyClasses
{
    public class GetSampleUser
    {
        public List<Users> UserList = new List<Users>();
        public List<Users> GetUser()
        {
            new Users
            {
                _UserName = "User01",
                _Password = "Pwd01"
            };

            new Users
            {
                _UserName = "User02",
                _Password = "Pwd02"
            };

            new Users
            {
                _UserName = "User03",
                _Password = "Pwd03"
            };

            new Users
            {
                _UserName = "User04",
                _Password = "Pwd04"
            };

            new Users
            {
                _UserName = "User05",
                _Password = "Pwd05"
            };

            new Users
            {
                _UserName = "User06",
                _Password = "Pwd06"
            };

            return UserList;
        }
    }
}
