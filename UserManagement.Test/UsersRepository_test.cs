using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManagement.Repository;
using UserManagement.Controllers;
using NSubstitute;
using UserManagement.Test.DummyClasses;
using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Test
{
    [TestClass]
    public class UserRepository_Test
    {
        private IUserRepository<Users> _ISearchUser;
        private Users _Users;
        private ISearchUser _IUserRepository;

        [TestMethod]
        public void LoginUserTestMethod()
        {
            //arrange
            LoginUserDC obj = new LoginUserDC();
            _ISearchUser = Substitute.For<IUserRepository<Users>>();
            var UController = new UsersController(_ISearchUser, _IUserRepository);
            _ISearchUser.Login(_Users).Returns(obj.LoginUserMethod("User06", "Pwd06"));

            //act
            string actual = _ISearchUser.Login(_Users);
            String expected = obj.LoginUserMethod("User06", "Pwd06");

            //assert
            Assert.AreEqual(actual, expected);

        }

        [TestMethod]
        public void GetAllUsersTest()
        {

            //arrange
            _Users = Substitute.For<Users>();
            GetSampleUser obj = new GetSampleUser();
            List<Users> CalledList = new List<Users>();
               CalledList =   obj.UserList;

            
            _ISearchUser = Substitute.For<IUserRepository<Users>>();
            var UController = new UsersController(_ISearchUser);
           // _IUserRepository.GetAllUser().Returns(CalledList);

            //act

            //assert
          //  Assert.AreEqual("Successfully Login", UController.GetAllUsers(Users userObj));

        }

       
    }
}
