using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using School.Helper;

namespace School.Models
{

    public class studentDetails
    {
        public int studentID { get; set; }

        public string? studentName { get; set; }

        public string? studentAddress { get; set; }

        public string? emailAddress { get; set; }

        public string? phoneNumber { get; set; }

        public string? age { get; set; }

        public string? studentGrade { get; set; }

        public string? studentGender { get; set; }

    }
}
