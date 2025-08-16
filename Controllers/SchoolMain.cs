using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using School.Models;
using System.Net.Http;
using School.BusinessLayer;

namespace School.Controllers
{
    public class SchoolMain : Controller
    {
        [HttpGet("student")]
        public IActionResult GetStudent()
        {
            Console.WriteLine("Step: 1 Student Page : " + DateTime.Now);

            return View("Student");
        }

        [HttpGet("employee")]
        public IActionResult GetEmployee()
        {
            Console.WriteLine("Step: 1 Employee Page : " + DateTime.Now);

            return View("Employee");
        }

        [HttpGet("login")]
        public IActionResult GetLogin()
        {
            Console.WriteLine("Step: 1 Login Page : " + DateTime.Now);

            return View("Login");
        }

        [HttpGet("logout")]
        public IActionResult GetLogout()
        {
            Console.WriteLine("Step: 1 Logout Page : " + DateTime.Now);

            HttpContext.Session.Remove("USERNAME");
            HttpContext.Session.Remove("FIRSTNAME");
            HttpContext.Session.Remove("LASTNAME");
            @ViewBag.Message = "Your session successfully logged out";
            return View("Message");
        }

        [HttpPost("emplogin")]
        public IActionResult GetEmpLogin([FromForm] Login logins)
        {
            employeeDetails empDetails1;
            EmployeeLayer std1 = new();

            Console.WriteLine("Step: 1 Login Page : " + DateTime.Now);
            empDetails1= std1.SelectEmployeeUserName(logins.empUserName);

            if (empDetails1.empPassword == logins.empPassword)
            {
                if (empDetails1.empPassUpdate == 0)
                {
                    @ViewBag.UserID = empDetails1.empUserName;
                    return View("SetPassword");
                }
                HttpContext.Session.SetString("USERNAME", empDetails1.empUserName);
                HttpContext.Session.SetString("FIRSTNAME", empDetails1.firstName);
                HttpContext.Session.SetString("LASTNAME", empDetails1.lastName);

                @ViewBag.UserData= empDetails1.firstName + " " + empDetails1.lastName;

                Response.Cookies.Append("UserAuthCookie", empDetails1.firstName + "_" + empDetails1.lastName);

                return View("GetMain");
            }
            else
            {
                @ViewBag.Message = "Invalid User Name or Password";
                return View("Error");
            }
            
        }

        [HttpGet("smain")]
        public IActionResult GetMain()
        {
            string? EMPUSERNAME;
            EMPUSERNAME = HttpContext.Session.GetString("USERNAME");
            Console.WriteLine("Step: 1 Main Page : " + DateTime.Now);

            if (EMPUSERNAME is not null)
            {

             

                Response.Cookies.Append("UserAuthCookie", "authenticated");


                @ViewBag.UserData = HttpContext.Session.GetString("FIRSTNAME") + " " + HttpContext.Session.GetString("LASTNAME");
                return View("GetMain");
            }
            else
            {
                return View("Login");
            }
            
        }

        [HttpGet("studentlist")]
        public IActionResult GetStudentlist()
        {
            Console.WriteLine("Step: 1 studentlist Page : " + DateTime.Now);

            StudentLayer std1 = new();
            List<studentDetails> students = std1.GetAllStudents();

            return View("StudentList", students);
        }

        [HttpGet("studentedit/{studentID}")]
        public IActionResult GetStudentForEdit(int studentID)
        {
            Console.WriteLine("Step: 1 studentedit Page : " + DateTime.Now);

            StudentLayer std1 = new();
            studentDetails student1 = std1.GetStudent(studentID);
            Console.WriteLine("Step: 2 studentedit Page : " + DateTime.Now + " " + student1.studentName);

            return View("StudentEdit", student1);
        }

        [HttpGet("employeelist")]
        public IActionResult GetEmployeelist()
        {
            Console.WriteLine("Step: 1 employeelist Page : " + DateTime.Now);

            EmployeeLayer std1 = new();
            List<employeeDetails> employee = std1.GetAllEmployee();

            return View("EmployeeList", employee);
        }

        [HttpGet("empdelete/{EMPID}")]
        public IActionResult DeleteEmployee(int EMPID)
        {
            EmployeeLayer std1 = new();
            string returnValue = "Employee Deleted : " + EMPID;
            returnValue = std1.DeleteEmployee(EMPID);


            Console.WriteLine("Employee Deleted : " + EMPID);

            @ViewBag.Message = "Empoyee deleted";
            return View("Message");
        }

        [HttpGet("empedit/{empUserName}")]
        public IActionResult GetEmpForEdit(string empUserName)
        {
            Console.WriteLine("Step: 1 EMPedit Page : " + DateTime.Now);

            EmployeeLayer std1 = new();
            employeeDetails emp1 = std1.SelectEmployeeID(empUserName);
            Console.WriteLine("Step: 2 empedit Page : " + DateTime.Now + " " + emp1.empUserName);

            return View("EmpEdit", emp1);
        }

        [HttpGet("empleave/")]
        public IActionResult EmpLeave()
        {


            Console.WriteLine("Step: 1 Employee Page : " + DateTime.Now);

            return View("Employee");

        }

    }
}
