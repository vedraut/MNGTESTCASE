using System;
using System.Runtime.Serialization;

namespace TestCase.Model
{
    public class User
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string DOB { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }

        [DataMember]
        public int Age { get; set; } = 0;

        [IgnoreDataMember]
        public DateTime InternalDateTime { get; set; }

    }
}

