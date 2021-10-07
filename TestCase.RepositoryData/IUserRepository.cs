using TestCase.Model;

namespace TestCase.RepositoryData
{
    //User Repository
    public interface IUserRepository
    {
        //Get User By Id
        User GetUserById(int userId);

        //Create User
        void CreateUser(User user);

        //Update User
        void UpdateUser(int userId, User user);

        //Delete User
        void DeleteUser(int userId);
    }
}
