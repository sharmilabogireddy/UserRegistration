using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace UserRegistrationForm
{
    public static class Crypto
    {
        public static String Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF7.GetBytes(value))
                );
        }
    }
}