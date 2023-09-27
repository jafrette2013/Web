using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace AGMU.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AgmuContext _agmuDbContext;

        public StudentsController(AgmuContext agmuDbContext)
        {
            this._agmuDbContext = agmuDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _agmuDbContext.Students
            .AsNoTracking() //This is a performance improvement
            .Select(t => new Student
            {
                Id = t.Id,
                Name = t.Name,
                DateOfBirth = t.DateOfBirth,
                PhoneNumber = t.PhoneNumber,
                ProgramId = t.ProgramId,
                AcademicProgram = new AcademicProgram
                {
                    Id = t.AcademicProgram.Id,
                    Name = t.AcademicProgram.Name
                }
            })
                .ToListAsync();
        }

        [HttpGet("ById")]
        public async Task<Student?> Get([FromQuery] int studentId)
        {
            return await _agmuDbContext.Students
            .AsNoTracking() //This is a performance improvement
            .Select(t => new Student
            {
                Id = t.Id,
                Name = t.Name,
                DateOfBirth = t.DateOfBirth,
                PhoneNumber = t.PhoneNumber,
                ProgramId = t.ProgramId,
                AcademicProgram = new AcademicProgram
                {
                    Id = t.AcademicProgram.Id,
                    Name = t.AcademicProgram.Name
                }
            })
                .FirstOrDefaultAsync(t => t.Id == studentId);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student request)
        {
            _ = _agmuDbContext.Students.Add(request);
            _ = await _agmuDbContext.SaveChangesAsync();
            return Ok();
        }



        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Student request)
        {
            var dbStudent = await _agmuDbContext.Students.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (dbStudent == null)
            {
                return NotFound("Invalid student Id");
            }
            dbStudent.PhoneNumber = request.PhoneNumber;
            dbStudent.ProgramId = request.ProgramId;
            dbStudent.Name = request.Name;
            dbStudent.DateOfBirth = request.DateOfBirth;
            _ = await _agmuDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int studentId)
        {
            var dbStudent = await _agmuDbContext.Students
              //  .Include(t => t.StudentClasses)
                .FirstOrDefaultAsync(t => t.Id == studentId);
            if (dbStudent == null)
            {
                return NotFound("Invalid student Id");
            }
            _ = _agmuDbContext.Students.Remove(dbStudent);
           // _agmuDbContext.StudentClasses.RemoveRange(dbStudent.StudentClasses);

            _ = await _agmuDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}