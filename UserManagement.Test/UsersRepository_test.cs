using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManagement.Repository;
using UserManagement.Controllers;
using NSubstitute;
using UserManagement.Test.DummyClasses;
using System.Collections.Generic;

namespace UserManagement.Test
{
    [TestClass]
    public class UserRepository_Test
    {
        private IUserRepository _IUserRepository;
        private UserModel _UserModel;

        [TestMethod]
        public void LoginUserTestMethod()
        {
            //arrange
            LoginUserDC obj = new LoginUserDC();
            _IUserRepository = Substitute.For<IUserRepository>();
            var UController = new UserController(_IUserRepository);
            _IUserRepository.Login("User06", "Pwd06").Returns(obj.LoginUserMethod("User06", "Pwd06"));

            //act
            string actual = _IUserRepository.Login("User06", "Pwd06");
            String expected = obj.LoginUserMethod("User06", "Pwd06");

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod]
        public void GetAllUsersTest()
        {

            //arrange
            _UserModel = Substitute.For<UserModel>();
            GetSampleUser obj = new GetSampleUser();
            List<UserModel> CalledList = new List<UserModel>();
               CalledList =   obj.UserList;

            
            _IUserRepository = Substitute.For<IUserRepository>();
            var UController = new UserController(_IUserRepository);
           // _IUserRepository.GetAllUser().Returns(CalledList);

            //act

            //assert
            Assert.AreEqual("Successfully Login", UController.GetAllUsers());

        }

       
    }
}
