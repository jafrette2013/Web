using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> myStudents = new List<Student>();

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return myStudents.OrderByDescending(t=> t.Id);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Student request)
        {
            myStudents.Add(request);
            return Ok();
        }
        [HttpPut]
        public ActionResult Put([FromBody] Student request)

        {
            if (myStudents.Any(t => t.Id == request.Id))
            {
                _ = myStudents.Remove(myStudents.First(t => t.Id == request.Id));
                myStudents.Add(request);
                return Ok();
            }
            return NotFound("This is an invalid student id, not found in memory !!");
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery]int studentId)
        {
            if (myStudents.Any(t => t.Id == studentId))
            {
                _ = myStudents.Remove(myStudents.First(t => t.Id == studentId));
                return Ok();
            }
            return NotFound("This is an invalid student id, not found in memory !!");
        }
    }
}