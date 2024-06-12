using StudentManagement.Models;
using StudentManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.Student
{
    public interface IStudentRepository
    {
       // public  StudentManagement.Models.Student AddStudent(StudentDto studentDto);
        public List<StudentManagement.Models.Student> AllStudents();

        public StudentManagement.Models.Student GetStudent(int studentId);

        public StudentManagement.Models.Student DeleteStudent(int id);

        public StudentManagement.Models.Student EditStudent(int studentId, StudentDto studentDto);
        StudentManagement.Models.Student AddStudent(StudentDto studentDto);
    }
}
