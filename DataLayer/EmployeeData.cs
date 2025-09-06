using Microsoft.Data.SqlClient;

using School.Helper;
using School.Models;
using System.Data;
using School.BusinessLayer;

namespace School.DataLayer
{
    public class EmployeeData
    {
        public string CreateEmployee(employeeDetails employee)
        {
            string SixDigitPassword;
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            EmailOTPLayer EOTP = new();

            SixDigitPassword = EOTP.Generate6DigitOTP();
            //int EMPID;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_CreateEmp", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    Console.WriteLine("Employee Created for : 1 ");
                    // Add parameters
                    command.Parameters.AddWithValue("@FirstName", employee.firstName);
                    command.Parameters.AddWithValue("@LastName", employee.lastName);
                    command.Parameters.AddWithValue("@EMPUserName", employee.empUserName);
                    command.Parameters.AddWithValue("@EMPpassword", SixDigitPassword);
                    command.Parameters.AddWithValue("@Age", employee.age);
                    command.Parameters.AddWithValue("@EmailAddress", employee.emailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.phoneNumber);
                    command.Parameters.AddWithValue("@YearJoined", employee.yearJoined);
                    Console.WriteLine("Employee Created for : 2 ");
                    // Output parameter

                    Console.WriteLine("Employee Created for : 3 ");
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    //EMPID=Convert.ToInt32(EMPIDParam.Value);
                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error inserting employee: {ex.Message}");
                //throw; // Re-throw for calling code to handle
                return "employee creation failed";
            }
            Console.WriteLine("Employee Created for : 4 ");

            EOTP.SendEmail(employee.emailAddress, "New Employe Creation", "Dear Employee please find your temporary password : " + SixDigitPassword);
            return "Employee successfully created ID : " + employee.empUserName;
        }

        public async Task<string> CreateEmployeeAsync(employeeDetails employee)
        {
            string SixDigitPassword;
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            EmailOTPLayer EOTP = new();

            SixDigitPassword = EOTP.Generate6DigitOTP();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_CreateEmp", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    Console.WriteLine("Employee Created for : 1 ");

                    // Add parameters (use Add instead of AddWithValue for better performance)
                    command.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar) { Value = employee.firstName });
                    command.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = employee.lastName });
                    command.Parameters.Add(new SqlParameter("@EMPUserName", SqlDbType.NVarChar) { Value = employee.empUserName });
                    command.Parameters.Add(new SqlParameter("@EMPpassword", SqlDbType.NVarChar) { Value = SixDigitPassword });
                    command.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int) { Value = employee.age });
                    command.Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar) { Value = employee.emailAddress });
                    command.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = employee.phoneNumber });
                    command.Parameters.Add(new SqlParameter("@YearJoined", SqlDbType.Int) { Value = employee.yearJoined });

                    Console.WriteLine("Employee Created for : 2 ");

                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Execute command asynchronously
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    Console.WriteLine("Employee Created for : 3 ");
                }
            }
            catch (Exception ex)
            {
                // Log error 
                Console.WriteLine($"Error inserting employee: {ex.Message}");
                return "employee creation failed";
            }

            Console.WriteLine("Employee Created for : 4 ");

            // Send email asynchronously (if SendEmail supports async)
            //await EOTP.SendEmailAsync(employee.emailAddress, "New Employee Creation",
               // $"Dear Employee please find your temporary password : {SixDigitPassword}");

            return $"Employee successfully created ID : {employee.empUserName}";
        }


        public string UpdateEmployee(employeeDetails employee)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_EmployeeUpdate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters

                    command.Parameters.AddWithValue("@FirstName", employee.firstName);
                    command.Parameters.AddWithValue("@LastName", employee.lastName);
                    command.Parameters.AddWithValue("@EMPUserName", employee.empUserName);
                    command.Parameters.AddWithValue("@EMPpassword", employee.empPassword);
                    command.Parameters.AddWithValue("@Age", employee.age);
                    command.Parameters.AddWithValue("@EmailAddress", employee.emailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.phoneNumber);
                    command.Parameters.AddWithValue("@YearJoined", employee.yearJoined);

                    // Output parameter


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error inserting employee: {ex.Message}");
                //throw; // Re-throw for calling code to handle
                return "employee updated failed";
            }

            return "employee successfully updated ID : ";
        }

        public string DeleteEmployee(int EMPID)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_EmpNewID_Delete", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@EMPID", EMPID);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error delete employee: {ex.Message}");
                throw; // Re-throw for calling code to handle
            }

            return "employee successfully deleted";
        }

        public List<employeeDetails> GetAllEmployee()
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            List<employeeDetails> employee = new List<employeeDetails>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_SelectAll", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employee.Add(new employeeDetails
                        {
                            empID = Convert.ToInt32(reader["empID"]),
                            firstName = reader["firstName"].ToString(),
                            lastName = reader["lastName"].ToString(),
                            empUserName = reader["empUserName"].ToString(),
                            empPassword = reader["empPassword"].ToString(),
                            age = reader["Age"].ToString(),
                            emailAddress = reader["emailAddress"].ToString(),
                            phoneNumber = reader["phoneNumber"].ToString(),
                            yearJoined = reader["yearJoined"].ToString(),
                            dateEmp = reader["DateEmp"].ToString()

                        });
                    }
                }
            }

            return employee;

        }

        public employeeDetails SelectEmployeeUserName(string username)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            employeeDetails emp = new employeeDetails();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_SelectUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empUserName", username);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();


                    emp.empID = Convert.ToInt32(reader["empID"]);
                    emp.firstName = reader["firstName"].ToString();
                    emp.lastName = reader["lastName"].ToString();
                    emp.empUserName = reader["empUserName"].ToString();
                    emp.empPassword = reader["empPassword"].ToString();
                    emp.age = reader["Age"].ToString();
                    emp.phoneNumber = reader["phoneNumber"].ToString();
                    emp.yearJoined = reader["yearJoined"].ToString();
                    emp.dateEmp = reader["DateEmp"].ToString();
                    emp.empPassUpdate = Convert.ToInt32(reader["PasswordUpdate"]);

                }
            }

            return emp;

        }

        public employeeDetails SelectEmployeeID(string empUserNames)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            employeeDetails emp1 = new employeeDetails();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_SelectUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empUserName", empUserNames);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();

                    emp1.firstName = reader["firstName"].ToString();
                    emp1.lastName = reader["lastName"].ToString();
                    emp1.empUserName = reader["empUserName"].ToString();
                    emp1.empPassword = reader["empPassword"].ToString();
                    emp1.age = reader["Age"].ToString();
                    emp1.emailAddress = reader["emailAddress"].ToString();
                    emp1.phoneNumber = reader["phoneNumber"].ToString();
                    emp1.yearJoined = reader["yearJoined"].ToString();

                }
            }

            return emp1;

        }

        public string SetPassword(userPassword emppass, int updatepass)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_EmployeeUpdatePass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters

                    command.Parameters.AddWithValue("@EMPUserName", emppass.empUserName);
                    command.Parameters.AddWithValue("@EMPpassword", emppass.password);
                    command.Parameters.AddWithValue("@PasswordUpdate", updatepass);


                    // Output parameter


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error inserting employee: {ex.Message}");
                //throw; // Re-throw for calling code to handle
                return "employee updated failed";
            }

            return "employee password updated ID : ";
        }

    }
}
