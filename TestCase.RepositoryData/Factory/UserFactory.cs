using System;
using System.Data;
using TestCase.Model;
using TestCase.RepositoryData.Core;

namespace TestCase.RepositoryData.Factory
{
    public class UserFactory
    {
        internal static User CreateUser(IDataReader dataReader)
        {
            User userResponse = new User()
            {
                FirstName = dataReader.GetValue<string>("FirstName", string.Empty),
                LastName = dataReader.GetValue<string>("LastName", string.Empty),
                DOB = dataReader.GetValue<DateTime?>("DateOfBirth", null)?.ToString("dd/MM/yyyy"),
                Gender = dataReader.GetValue<string>("Gender", string.Empty),
                EmailAddress = dataReader.GetValue<string>("Email", string.Empty),
                ContactNumber = dataReader.GetValue<string>("ContactNumber", string.Empty),
                InternalDateTime = dataReader.GetValue<DateTime>("DateOfBirth", DateTime.MaxValue)
            };

            return userResponse;
        }
    }
}
