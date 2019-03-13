using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Test.DummyClasses
{
    class LoginUserDC
    {
        public string LoginUserMethod(string UN, string PW)
        {
            GetSampleUser obj = new GetSampleUser();
            List <UserModel> CalledList = obj.GetUser();

            try
            {
                foreach (UserModel user in CalledList)
                {
                    UserModel UserObj = new UserModel();
                    UserObj.UserName = CalledList.Find(User1 => User1.UserName == UN).ToString();
                    UserObj.password = CalledList.Find(User2 => User2.password == PW).ToString();
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
