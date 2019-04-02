using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Test.DummyClasses
{
    class LoginUserDC
    {
        public string LoginUserMethod(string UN, string PW)
        {
            GetSampleUser obj = new GetSampleUser();
            List <Users> CalledList = obj.GetUser();

            try
            {
                foreach (Users user in CalledList)
                {
                    Users UserObj = new Users();
                    UserObj._UserName = CalledList.Find(User1 => User1._UserName == UN).ToString();
                    UserObj._Password = CalledList.Find(User2 => User2._Password == PW).ToString();
                }
                return "Sucessfull login";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}
