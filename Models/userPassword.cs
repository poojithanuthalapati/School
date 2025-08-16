using Microsoft.Data.SqlClient;
using School.Helper;
using System.Data;

namespace School.Models
{
    public class userPassword
    {
        public string? empUserName { get; set; }

        public string? password { get; set; }

        public string? confirmPassword { get; set; }


    }

}
