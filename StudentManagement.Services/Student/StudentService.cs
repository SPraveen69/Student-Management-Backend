using StudentManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Services.Student
{
    public class StudentService : IStudentRepository
    {
        StudentManagement.Models.Student IStudentRepository.AddStudent(StudentDto studentDto)
        {
            throw new NotImplementedException();
        }

        List<StudentManagement.Models.Student> IStudentRepository.AllStudents()
        {
            throw new NotImplementedException();
        }

        StudentManagement.Models.Student IStudentRepository.DeleteStudent(StudentManagement.Models.Student student)
        {
            throw new NotImplementedException();
        }

        StudentManagement.Models.Student IStudentRepository.EditStudent(int studentId, StudentDto studentDto)
        {
            throw new NotImplementedException();
        }

        StudentManagement.Models.Student IStudentRepository.GetStudent(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
