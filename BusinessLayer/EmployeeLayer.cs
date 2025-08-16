using School.Models;
using School.DataLayer;

namespace School.BusinessLayer
{
    public class EmployeeLayer
    {
        public string SetPassword(userPassword emppass, int updatepass)
        {
            EmployeeData empdata = new();

            return empdata.SetPassword(emppass, updatepass);
        }

        public string CreateEmployee(employeeDetails employee)
        {
            EmployeeData empdata = new();

            return empdata.CreateEmployee(employee);
        }

        public string UpdateEmployee(employeeDetails employee)
        {
            EmployeeData empdata = new();

            return empdata.UpdateEmployee(employee);
        }

        public string DeleteEmployee(int EMPID)
        {
            EmployeeData empdata = new();

            return empdata.DeleteEmployee(EMPID);
        }

        public List<employeeDetails> GetAllEmployee()
        {
            EmployeeData empdata = new();

            return empdata.GetAllEmployee();
        }

        public employeeDetails SelectEmployeeUserName(string username)
        {
            EmployeeData empdata = new();

            return empdata.SelectEmployeeUserName(username);
        }

        public employeeDetails SelectEmployeeID(string empUserNames)
        {
            EmployeeData empdata = new();

            return empdata.SelectEmployeeID(empUserNames);
        }
    }
}
