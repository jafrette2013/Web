using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace AGMU.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcademicProgramsController : ControllerBase
    {
        private readonly AgmuContext _agmuDbContext;

        public AcademicProgramsController(AgmuContext agmuDbContext)
        {
            this._agmuDbContext = agmuDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<AcademicProgram>> Get()
        {
            return await _agmuDbContext.AcademicPrograms
            .AsNoTracking()
            .Select(t => new AcademicProgram
            {
                Id = t.Id,
                Name = t.Name
            })
            .ToListAsync();
        }

        [HttpGet("ById")]
        public async Task<AcademicProgram?> Get([FromQuery] int academicProgramId)
        {
            return await _agmuDbContext.AcademicPrograms
            .AsNoTracking() 
            .Select(t => new AcademicProgram
            {
                Id = t.Id,
                Name = t.Name
            })
            .FirstOrDefaultAsync(t => t.Id == academicProgramId);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AcademicProgram request)
        {
            _ = _agmuDbContext.AcademicPrograms.Add(request);
            _ = await _agmuDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AcademicProgram request)
        {
            var dbAcademicProgram = await _agmuDbContext.AcademicPrograms.FirstOrDefaultAsync(t => t.Id == request.Id);
            if (dbAcademicProgram == null)
            {
                return NotFound("Invalid academic program Id");
            }
            dbAcademicProgram.Name = request.Name;
            _ = await _agmuDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int academicProgramId)
        {
            var dbAcademicProgram = await _agmuDbContext.AcademicPrograms.FirstOrDefaultAsync(t => t.Id == academicProgramId);
            if (dbAcademicProgram == null)
            {
                return NotFound("Invalid academic program Id");
            }
            _ = _agmuDbContext.AcademicPrograms.Remove(dbAcademicProgram);
            _ = await _agmuDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
