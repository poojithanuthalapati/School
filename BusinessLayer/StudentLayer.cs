using School.Models;
using School.DataLayer;

namespace School.BusinessLayer


{
    public class StudentLayer
    {
        public string CreateStudent(studentDetails student)
        {
            StudentData studata = new();

            return studata.CreateStudent( student);
        }

        public string UpdateStudent(studentDetails student)
        {
            StudentData studata = new();

            return studata.UpdateStudent( student);
        }

        public string DeleteStudent(int studentID)
        {
            StudentData studata = new();

            return studata.DeleteStudent(studentID);
        }

        public List<studentDetails> GetAllStudents()
        {
            StudentData studata = new();

            return studata.GetAllStudents();
        }

        public studentDetails GetStudent(int studentIDs)
        {
            StudentData studata = new();

            return studata.GetStudent(studentIDs);
        }
    }
}
