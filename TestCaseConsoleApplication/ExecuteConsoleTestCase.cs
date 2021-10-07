using System;
using System.Globalization;
using System.Text.RegularExpressions;
using TestCase.Business;
using TestCase.Model;

namespace TestCaseConsoleApplication
{
    public class ExecuteConsoleTestCase
    {
        private readonly IUserManager _userManager;

        public ExecuteConsoleTestCase(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public static void Main(string[] args)
        {
            User user = new User();
            try
            {
                FirstName(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                FirstName(user);
            }

            try
            {
                LastName(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                LastName(user);
            }

            try
            {
                DOB(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                DOB(user);
            }

            try
            {
                Console.WriteLine("Please enter your Gender from Male/Female/Others");
                var gender = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            try
            {
                Email(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Email(user);
            }

            try
            {
                ContactNumber(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ContactNumber(user);
            }

            Console.WriteLine(user.Age);
        }

        private static void DOB(User user)
        {
            Console.WriteLine("Please enter Date of Birth in format DD/MM/YYYY");
            user.DOB = Console.ReadLine();
            if (user.DOB.Length > 20)
            {
                throw new ArgumentException("should not exceed 20 characters");
            }

            bool validDOB = DateTime.TryParseExact(user.DOB, "dd/MM/yyyy",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None,
                                                out DateTime parsed);

            var age = DateTime.Now.Subtract(Convert.ToDateTime(user.DOB)).Days;
            user.Age = age / 365;

            if (!validDOB)
            {
                throw new ArgumentException("should be a valid date", nameof(user.DOB));
            }
        }

        private static void Email(User user)
        {
            Console.WriteLine("Please enter a valid Email Address");
            user.EmailAddress = Console.ReadLine();
            var addr = new System.Net.Mail.MailAddress(user.EmailAddress);
            var validEmail = addr.Address == user.EmailAddress;
            if (!validEmail)
            {
                throw new ArgumentException("should be a valid email address", nameof(user.EmailAddress));
            }
        }

        private static void ContactNumber(User user)
        {
            Console.WriteLine("Please enter your Contact Number");
            user.ContactNumber = Console.ReadLine();

            if (Regex.IsMatch(user.ContactNumber, "\\A[0-9]{10}\\z"))
            {
                throw new ArgumentException("should be a 10 digit number", nameof(user.ContactNumber));
            }
        }

        private static void LastName(User user)
        {
            Console.WriteLine("Please Enter Last Name");
            var lastName = Console.ReadLine();

            if (user.FirstName.Length > 20)
            {
                throw new ArgumentException("should not exceed 20 characters");
            }
        }

        private static void FirstName(User user)
        {
            Console.WriteLine("Please enter First Name");

            user.FirstName = Console.ReadLine();
            if (user.FirstName.Length > 20)
            {
                throw new ArgumentException("should not exceed 20 characters");
            }
        }
    }
}

