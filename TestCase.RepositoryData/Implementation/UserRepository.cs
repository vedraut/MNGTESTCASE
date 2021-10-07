using System;
using System.Data;
using System.Data.SqlClient;
using TestCase.Model;
using TestCase.RepositoryData.Core;
using TestCase.RepositoryData.Factory;

namespace TestCase.RepositoryData.Implementation
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        #region GetUser
        public User GetUserById(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException("UserId cannot be less then 1");
            }

            User userResponse = new User();
            var getCommand = new SqlCommand
            {
                CommandText = "dbo.GetUserById",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            getCommand.Parameters.AddWithValue("@userId", userId);

            try
            {
                OpenConnection();
                using (IDataReader dataReader = getCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        userResponse = UserFactory.CreateUser(dataReader);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }

            return userResponse;
        }

        #endregion

        #region CreateUser
        public void CreateUser(User user)
        {
            var createCommand = new SqlCommand
            {
                CommandText = "dbo.CreateUser",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };

            createCommand.Parameters.AddWithValue("@firstName", user.FirstName);
            createCommand.Parameters.AddWithValue("@lastName", user.LastName);
            createCommand.Parameters.AddWithValue("@dateOfBirth", user.InternalDateTime);
            createCommand.Parameters.AddWithValue("@gender", user.Gender);
            createCommand.Parameters.AddWithValue("@email", user.EmailAddress);
            createCommand.Parameters.AddWithValue("@contactNumber", user.ContactNumber);

            try
            {
                OpenConnection();
                createCommand.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region UpdateUser

        public void UpdateUser(int userId, User user)
        {
            var updateCommand = new SqlCommand
            {
                CommandText = "dbo.UpdateUser",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            updateCommand.Parameters.AddWithValue("@userId", userId);
            if (user.FirstName != "")
            {
                updateCommand.Parameters.AddWithValue("@firstName", user.FirstName);
            }
            if (user.LastName != "")
            {
                updateCommand.Parameters.AddWithValue("@lastName", user.LastName);
            }
            if (user.DOB != null)
            {
                updateCommand.Parameters.AddWithValue("@dateOfBirth", user.InternalDateTime);
            }
            if (user.Gender != "")
            {
                updateCommand.Parameters.AddWithValue("@gender", user.Gender);
            }
            if (user.EmailAddress == "")
            {
                updateCommand.Parameters.AddWithValue("@email", user.EmailAddress);
            }
            if (user.ContactNumber != "")
            {
                updateCommand.Parameters.AddWithValue("@contactNumber", user.ContactNumber);
            }

            try
            {
                OpenConnection();
                updateCommand.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region DeleteUser

        public void DeleteUser(int userId)
        {
            var command = new SqlCommand
            {
                CommandText = "dbo.DeleteUser",
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            command.Parameters.AddWithValue("@userId", userId);
            try
            {
                OpenConnection();
                command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion


    }
}
