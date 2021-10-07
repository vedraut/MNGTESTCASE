using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TestCase.Model;
using TestCase.RepositoryData;
using TestCase.RepositoryData.Implementation;

namespace TestCase.Business.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region GetUser

        public User GetUserById(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException("Invalid User Id");
            }

            var result = _userRepository.GetUserById(userId);
            result.Age = AgeCalculator(result.InternalDateTime);

            return result;
        }

        #endregion

        #region CreateUser

        public bool CreateUser(User user)
        {
            UserValidator(user);
            _userRepository.CreateUser(user);

            return true;
        }

        #endregion

        #region UpdateUser

        public bool UpdateUser(int userId, User user)
        {
            if (userId < 1)
            {
                throw new ArgumentException("Invalid User Id");
            }

            UserValidator(user);

            _userRepository.UpdateUser(userId, user);

            return true;

        }

        #endregion

        #region DeleteUser

        public bool DeleteUser(int userId)
        {
            if (userId < 1)
            {
                throw new ArgumentException("Invalid User Id");
            }
            _userRepository.DeleteUser(userId);

            return true;
        }

        #endregion

        #region Validation
        private void UserValidator(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Please input valid user has no value, user cannot be null.", nameof(user));
            }

            if (user.FirstName.Length > 20 || user.LastName.Length > 20)
            {
                throw new ArgumentException("should not exceed 20 characters");
            }

            if (Regex.IsMatch(user.ContactNumber, "\\A[0-9]{10}\\z"))
            {
                throw new ArgumentException("should be a 10 digit number", nameof(user.ContactNumber));
            }

            DateTime parsed;
            bool validDOB = DateTime.TryParseExact(user.DOB, "dd/MM/yyyy",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None,
                                                out parsed);
            user.InternalDateTime = parsed;

            if (!validDOB)
            {
                throw new ArgumentException("should be a valid date", nameof(user.DOB));
            }

            var addr = new System.Net.Mail.MailAddress(user.EmailAddress);
            var validEmail = addr.Address == user.EmailAddress;
            if (!validEmail)
            {
                throw new ArgumentException("should be a valid email address", nameof(user.EmailAddress));
            }
        }

        #endregion

        #region Miscellaneous 

        private int AgeCalculator(DateTime dateTime)
        {
            var age = DateTime.Now.Subtract(dateTime).Days;
            age = age / 365;
            return age;
        }

        #endregion

    }
}
