using SimplePassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MEMBER.Models
{
    public class PasswordHashModel
    {
        //Hash password method
        public static string Hash(string password)
        {
            var passwordHash = new SaltedPasswordHash(password, 20);
            return passwordHash.Hash + ":" + passwordHash.Salt;

        }

        //verify password method
        public static bool Verify(string password, string passwordHash)
        {
            string[] _passwordHash = passwordHash.Split(':');
            if(_passwordHash.Length == 2)
            {
                var saltedPasswordHash = new SaltedPasswordHash(_passwordHash[0], _passwordHash[1]);
                return saltedPasswordHash.Verify(password);
            }

            return false;
        }
    }
}