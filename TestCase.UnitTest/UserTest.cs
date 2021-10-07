using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using TestCase.Business;
using TestCase.Business.Implementation;
using TestCase.Model;
using TestCase.RepositoryData;

namespace TestCase.UnitTest
{
    [TestClass]
    public class UserTest
    {
        private Mock<IUserRepository> _mockUserRepository;
        private IUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userManager = new UserManager(_mockUserRepository.Object);
        }

        #region GetUser

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetUserById_Exception_UserId_Invalid()
        {
            //Arrange
            int userId = 0;
            //Act
            _userManager.GetUserById(userId);
            //Assert
        }

        [Test]
        public void GetUserById_Success()
        {
            //Arrange
            int userId = 1;
            _mockUserRepository.Setup(x => x.GetUserById(It.IsAny<int>()));
            //Act
            _userManager.GetUserById(userId);
        }

        #endregion

        #region CreateUser

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void CreateUser_Exception_UserusNull()
        {
            //Arrange
            User user = null;
            //Act
            _userManager.CreateUser(user);

        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CreateUser_Exception_Invalid_FirstName()
        {
            //Arrange
            User user = new User()
            {
                FirstName = "askdjaskdjasdkjlaskjdljaskdljaas",
            };
            //Act
            _userManager.CreateUser(user);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CreateUser_Exception_Unvalid_LastName()
        {
            //Arrange
            User user = new User()
            {
                LastName = "askdjaskdjasdkjlaskjdljaskdljaas",
            };
            //Act
            _userManager.CreateUser(user);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CreateUser_Exception_Invalid_DOB()
        {
            //Arrange
            User user = new User()
            {
                FirstName = "askdjas",
                LastName = "askdjasj",
                DOB = "askdjaskdjasdkjlaskjdljaskdljaas",
            };
            //Act
            _userManager.CreateUser(user);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CreateUser_Exception_Invalid_Email()
        {
            //Arrange
            User user = new User()
            {
                FirstName = "askdjas",
                LastName = "askdjasj",
                DOB = "11/11/1975",
                EmailAddress = "jasdiasjd.casdjalmdka"
            };
            //Act
            _userManager.CreateUser(user);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void CreateUser_Exception_Invalid_ContactNumber()
        {
            //Arrange
            User user = new User()
            {
                FirstName = "askdjas",
                LastName = "askdjasj",
                DOB = "11/11/1975",
                EmailAddress = "jasdiasjd@gmail.com",
                Gender = "Male",
                ContactNumber = "123213213213321312321313"
                
            };
            //Act
            _userManager.CreateUser(user);
        }

        [Test]
        public void CreateUser_Success()
        {
            //Arrange
            User user = new User()
            {
                FirstName = "askdjas",
                LastName = "askdjasj",
                DOB = "11/11/1975",
                EmailAddress = "jasdiasjd@gmail.com",
                Gender = "Male",
                ContactNumber = "1232132132"
            };
            //Act
            _userManager.CreateUser(user);
        }

        #endregion
    }
}

