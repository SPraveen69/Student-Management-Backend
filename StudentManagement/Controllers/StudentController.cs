using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DataAccess;
using StudentManagement.Models;
using StudentManagement.Services.Models;
using StudentManagement.Services.Student;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly SMDbContext _SMDBContext;
        public StudentController(IStudentRepository studentRepository, SMDbContext sMDbContext)
        {
            this._studentRepository = studentRepository;
            this._SMDBContext = sMDbContext;
        }

        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent([FromForm] StudentDto studentDto)
        {
            try
            {
                var student = new Student
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
                    Photo = await ConvertIFormFileToByteArray(studentDto.Photo)
                };

                _SMDBContext.Students.Add(student);
                await _SMDBContext.SaveChangesAsync();
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        private async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
{
    if (file == null || file.Length == 0) return null;
    using (var memoryStream = new MemoryStream())
    {
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
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
        [Route("DeleteStudent/{id}")]
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
