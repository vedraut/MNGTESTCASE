using System;
using TestCase.Model;

namespace TestCase.Business
{
    public interface IUserManager
    {
        //Get User By Id
        User GetUserById(int userId);

        //Create User
        bool CreateUser(User user);

        //Update User
        bool UpdateUser(int userId, User user);

        //Delete User
        bool DeleteUser(int userId);
    }
}
