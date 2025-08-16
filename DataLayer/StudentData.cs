using Microsoft.Data.SqlClient;
using School.Helper;
using School.Models;
using System.Data;

namespace School.DataLayer
{
    public class StudentData
    {
        public string CreateStudent(studentDetails student)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            int StudentID;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_StudentNew_Insert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@StudentName", student.studentName);
                    command.Parameters.AddWithValue("@StudentAddress", student.studentAddress);
                    command.Parameters.AddWithValue("@EmailAddress", student.emailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", student.phoneNumber);
                    command.Parameters.AddWithValue("@Age", student.age);
                    command.Parameters.AddWithValue("@StudentGrade", student.studentGrade);
                    command.Parameters.AddWithValue("@StudentGender", student.studentGender);

                    // Output parameter
                    var studentIdParam = new SqlParameter("@StudentID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(studentIdParam);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    StudentID = Convert.ToInt32(studentIdParam.Value);
                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error inserting student: {ex.Message}");
                //throw; // Re-throw for calling code to handle
                return "student createion failed";
            }

            return "Student successfully created ID : " + StudentID.ToString();
        }

        public string UpdateStudent(studentDetails student)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            int StudentID;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_StudentUpdate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@StudentName", student.studentName);
                    command.Parameters.AddWithValue("@StudentAddress", student.studentAddress);
                    command.Parameters.AddWithValue("@EmailAddress", student.emailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", student.phoneNumber);
                    command.Parameters.AddWithValue("@Age", student.age);
                    command.Parameters.AddWithValue("@StudentGrade", student.studentGrade);
                    command.Parameters.AddWithValue("@StudentGender", student.studentGender);
                    command.Parameters.AddWithValue("@StudentID", student.studentID);

                    // Output parameter


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    //StudentID = Convert.ToInt32(studentIdParam.Value);
                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error inserting student: {ex.Message}");
                //throw; // Re-throw for calling code to handle
                return "student updated failed";
            }

            return "Student successfully updated ID : ";
        }

        public string DeleteStudent(int studentID)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_StudentNew_Delete_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@StudentID", studentID);


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    //return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                // Log error (consider using a logging framework)
                Console.WriteLine($"Error delete student: {ex.Message}");
                throw; // Re-throw for calling code to handle
            }

            return "student successfully deleted";
        }

        public List<studentDetails> GetAllStudents()
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            List<studentDetails> students = new List<studentDetails>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_StudentNew_Select_All", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new studentDetails
                        {
                            studentID = Convert.ToInt32(reader["StudentID"]),
                            studentName = reader["StudentName"].ToString(),
                            studentAddress = reader["StudentAddress"].ToString(),
                            emailAddress = reader["EmailAddress"].ToString(),
                            phoneNumber = reader["PhoneNumber"].ToString(),
                            age = reader["Age"].ToString(),
                            studentGrade = reader["StudentGrade"].ToString(),
                            studentGender = reader["StudentGender"].ToString()

                        });
                    }
                }
            }

            return students;

        }

        public studentDetails GetStudent(int studentIDs)
        {
            string connectionString = ConfigurationHelper.GetValue("ConnectionString");
            studentDetails student1 = new studentDetails();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_StudentNew_Select_ID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", studentIDs);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();


                    student1.studentID = Convert.ToInt32(reader["StudentID"]);
                    student1.studentName = reader["StudentName"].ToString();
                    student1.studentAddress = reader["StudentAddress"].ToString();
                    student1.emailAddress = reader["EmailAddress"].ToString();
                    student1.phoneNumber = reader["PhoneNumber"].ToString();
                    student1.age = reader["Age"].ToString();
                    student1.studentGrade = reader["StudentGrade"].ToString();
                    student1.studentGender = reader["StudentGender"].ToString();

                }
            }

            return student1;

        }
    }
}
