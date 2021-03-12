using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADIntegration
{
    class User
    {
        public string firstname;
        public string name;
        public string mail;
        public string mobile;
        public string telephone;
        public string streetAddress;
        public string postalCode;
        public string memberOf;
        public User(string FirstName, string Name, string Mail, string Mobile, string Telephone, string StreetAddress, string PostalCode, string MemberOf)
        {
            firstname = FirstName;
            name = Name;
            mail = Mail;
            mobile = Mobile;
            telephone = Telephone;
            streetAddress = StreetAddress;
            postalCode = PostalCode;
            memberOf = MemberOf;
        }
    }
}
