using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using School.Helper;
using School.BusinessLayer;

namespace School.Models
{

    public class employeeDetails
    {
        public int empID { get; set; }

        public string? firstName { get; set; }

        public string? lastName { get; set; }

        public string? empUserName { get; set; }

        public string? empPassword { get; set; }

        public string? age { get; set; }

        public string? emailAddress { get; set; }

        public string? phoneNumber { get; set; }

        public string? yearJoined { get; set; }

        public string? dateEmp { get; set; }

        public int empPassUpdate { get; set; }

       
    }
}
