using StudentManagement.DataAccess;
using StudentManagement.Models;
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
        private readonly SMDbContext _context = new SMDbContext();
        StudentManagement.Models.Student IStudentRepository.AddStudent(StudentDto studentDto)
        {
            var student = new StudentManagement.Models.Student
            {
                Id = studentDto.Id,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                NIC = studentDto.NIC,
                DOB = studentDto.DOB,
                Address = studentDto.Address,
                Status = Status.Active,
                last_sync_date_time = DateTime.Now,
               // Photo = studentDto.Photo
            };
            _context.Add(student);
            _context.SaveChanges();
            return student;
        }

        List<StudentManagement.Models.Student> IStudentRepository.AllStudents()
        {
            return _context.Students.ToList();
        }

        StudentManagement.Models.Student IStudentRepository.DeleteStudent(int id)
        {
            var remStudent = _context.Students.FirstOrDefault(r => r.Id == id);

            if (remStudent != null) 
            {
                remStudent.Status = Status.Inactive;
                _context.SaveChanges();
            }
            return remStudent;
        }

        StudentManagement.Models.Student IStudentRepository.EditStudent(int studentId, StudentDto studentDto)
        {
            var existStudent = _context.Students.Find(studentId);
            if (existStudent != null)
            {
                existStudent.Id = studentId;
                existStudent.FirstName = studentDto.FirstName;
                existStudent.LastName = studentDto.LastName;
                existStudent.Email = studentDto.Email;
                existStudent.Phone = studentDto.Phone;
                existStudent.NIC = studentDto.NIC;
                existStudent.DOB = studentDto.DOB;
                existStudent.Address = studentDto.Address;
                existStudent.last_sync_date_time = DateTime.Now;
                //existStudent.Photo = studentDto.Photo;

                _context.SaveChanges();
                return existStudent;
            }
            else 
            {
                throw new Exception("Student not found with the ID : " + studentId);
            }
            
        }

        StudentManagement.Models.Student IStudentRepository.GetStudent(int studentId)
        {
            return _context.Students.Find(studentId);
        }
    }
}
