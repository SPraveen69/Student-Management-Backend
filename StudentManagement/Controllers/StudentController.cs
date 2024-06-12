using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Services.Models;
using StudentManagement.Services.Student;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddStudent(StudentDto studentDto)
        {
            var student = _studentRepository.AddStudent(studentDto);
            return Ok(student);
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var AllStudents = _studentRepository.AllStudents();
            if (AllStudents == null)
            {
                return NotFound();
            }
            return Ok(AllStudents);
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult DeleteStudent(int id)
        {
            var delStudent = _studentRepository.DeleteStudent(id);
            if (delStudent == null)
            {
                return NotFound();
            }
            return Ok(delStudent);
        }

        [HttpPut]
        [Route("EditStudent/{studentId}")]
        public IActionResult EditStudent(int studentId, StudentDto studentDto)
        {
            var editStudent = _studentRepository.EditStudent(studentId, studentDto);
            if(editStudent == null)
            {
                return NotFound();
            }
            return Ok(editStudent);

        }

        [HttpGet]
        [Route("GetStudent")]
        public IActionResult GetStudent(int studentId)
        {
            var GetStudent = _studentRepository.GetStudent(studentId);
            if (GetStudent == null)
            {
                return NotFound();
            }
            return Ok(GetStudent);
        }
    }
}
