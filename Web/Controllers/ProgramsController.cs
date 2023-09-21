using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using models = Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private static List<models.Program> myPrograms = new List<models.Program>();

        [HttpGet]
        public IEnumerable<models.Program> Get()
        {
            return myPrograms.OrderByDescending(t => t.Id);
        }

        [HttpPost]
        public ActionResult Post([FromBody] models.Program request)
        {
            myPrograms.Add(request);
            return Ok();
        }
        [HttpPut]
        public ActionResult Put([FromBody] models.Program request)

        {
            if (myPrograms.Any(t => t.Id == request.Id))
            {
                _ = myPrograms.Remove(myPrograms.First(t => t.Id == request.Id));
                myPrograms.Add(request);
                return Ok();
            }
            return NotFound("This is an invalid student id, not found in memory !!");
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int programId)
        {
            if (myPrograms.Any(t => t.Id == programId))
            {
                _ = myPrograms.Remove(myPrograms.First(t => t.Id == programId));
                return Ok();
            }
            return NotFound("This is an invalid student id, not found in memory !!");
        }
    }
}
