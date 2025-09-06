using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using School.Models;
using School.BusinessLayer;

namespace School.Controllers
{
    [Route("subscription")]
    [ApiController]
    public class subscriptionUser : ControllerBase
    {
        [HttpGet("studentdelete/{studentID}")]
        public string DeleteStudent(int studentID)
        {
            StudentLayer std = new();
            string returnValue;
            returnValue = std.DeleteStudent(studentID);


            Console.WriteLine("Student Deleted : " + studentID);


            return returnValue;
        }

        [HttpPost("studentclass")]
        public string CreateStudent([FromForm] studentDetails std)
        {
            StudentLayer std1 = new();
            string returnValue;
            returnValue = std1.CreateStudent(std);


            Console.WriteLine("Student Created for : " + std.studentName);


            return returnValue;
        }

        [HttpPost("studentupdate")]
        public string UpdateStudent([FromForm] studentDetails std)
        {
            StudentLayer std1 = new();
            string returnValue;
            returnValue = std1.UpdateStudent(std);


            Console.WriteLine("Student Created for : " + std.studentName);


            return returnValue;
        }

        [HttpPost("empcreate")]
        public string CreateEmployee([FromForm] employeeDetails std)
        {
            EmployeeLayer std1 = new();
            string returnValue;
            returnValue = std1.CreateEmployee(std);


            Console.WriteLine("Employee Created for : " + std.empUserName);


            return returnValue;
        }

        [HttpPost("empcreateasync")]
        public async Task<ActionResult<string>> CreateEmployeeAsync([FromForm] employeeDetails std)
        {
            EmployeeLayer std1 = new();
            string returnValue = await std1.CreateEmployeeAsync(std);

            Console.WriteLine("Employee Created for : " + std.empUserName);

            return returnValue;
        }

        [HttpPost("setpassword")]
        public string SetPassword([FromForm] userPassword std)
        {
            EmployeeLayer emplater = new();
            string returnValue;
            returnValue = emplater.SetPassword(std,1);


            Console.WriteLine("Employee password for : " + std.empUserName);


            return returnValue;
        }

        [HttpGet("resetpassword/{EMPUser}")]
        public string ResetPassword(string EMPUser)
        {
            Console.WriteLine("Employee password for 1 : " + EMPUser);

            string SixDigitPassword;
            EmailOTPLayer EOTP = new();
            SixDigitPassword = EOTP.Generate6DigitOTP();

            Console.WriteLine("Employee password for 2 : " + SixDigitPassword);

            string returnValue;
            userPassword userpass = new();
            EmployeeLayer emplayer = new();
            userpass.empUserName = EMPUser;
            userpass.password = SixDigitPassword;
            returnValue = emplayer.SetPassword(userpass,0);


            Console.WriteLine("Employee password for :3 " + EMPUser);


            return returnValue;
        }

        [HttpGet("empdelete/{EMPID}")]
        public string DeleteEmployee(int EMPID)
        {
            EmployeeLayer std1 = new();
            string returnValue = "Employee Deleted : " + EMPID;
            returnValue = std1.DeleteEmployee(EMPID);


            Console.WriteLine("Employee Deleted : " + EMPID);


            return returnValue;
        }

        [HttpPost("employeeupdate")]
        public string UpdateEmployee([FromForm] employeeDetails emp)
        {
            EmployeeLayer std1 = new();
            string returnValue;
            returnValue = std1.UpdateEmployee(emp);


            Console.WriteLine("Employee update for : " + emp.firstName);


            return returnValue;
        }

        [HttpGet("employeeall")]
        public List<employeeDetails> GetAllEmployee()
        {
            List<employeeDetails> returnValue;
            EmployeeLayer std1 = new();
            returnValue = std1.GetAllEmployee();


            Console.WriteLine("Employee GetAll : " + returnValue.Count.ToString());


            return returnValue;
        }
    }
}
