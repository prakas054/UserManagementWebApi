using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Test.DummyClasses
{
    public class UserModel
    {
        public string UserName { get; set;}
        public string password { get; set; }

    }
    public class GetSampleUser
    {
        public List<UserModel> UserList = new List<UserModel>();
        public List<UserModel> GetUser()
        {
            new UserModel
            {
                UserName = "User01",
                password = "Pwd01"
            };

            new UserModel
            {
                UserName = "User02",
                password = "Pwd02"
            };

            new UserModel
            {
                UserName = "User03",
                password = "Pwd03"
            };

            new UserModel
            {
                UserName = "User04",
                password = "Pwd04"
            };

            new UserModel
            {
                UserName = "User05",
                password = "Pwd05"
            };

            new UserModel
            {
                UserName = "User06",
                password = "Pwd06"
            };

            return UserList;
        }
    }
}
