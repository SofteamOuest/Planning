using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planning.Models;

namespace Planning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskLinesController : ControllerBase
    {
        private readonly PlanningContext _context;

        public ProjectTaskLinesController(PlanningContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTaskLines
        [HttpGet]
        public IEnumerable<ProjectTaskLine> GetProjectTaskLine()
        {
            return _context.ProjectTaskLine;
        }

        // GET: api/ProjectTaskLines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTaskLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTaskLine = await _context.ProjectTaskLine.FindAsync(id);

            if (projectTaskLine == null)
            {
                return NotFound();
            }

            return Ok(projectTaskLine);
        }

        // PUT: api/ProjectTaskLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTaskLine([FromRoute] int id, [FromBody] ProjectTaskLine projectTaskLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTaskLine.ProjectTaskId)
            {
                return BadRequest();
            }

            _context.Entry(projectTaskLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProjectTaskLines
        [HttpPost]
        public async Task<IActionResult> PostProjectTaskLine([FromBody] ProjectTaskLine projectTaskLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectTaskLine.Add(projectTaskLine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectTaskLineExists(projectTaskLine.ProjectTaskId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjectTaskLine", new { id = projectTaskLine.ProjectTaskId }, projectTaskLine);
        }

        // DELETE: api/ProjectTaskLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTaskLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTaskLine = await _context.ProjectTaskLine.FindAsync(id);
            if (projectTaskLine == null)
            {
                return NotFound();
            }

            _context.ProjectTaskLine.Remove(projectTaskLine);
            await _context.SaveChangesAsync();

            return Ok(projectTaskLine);
        }

        private bool ProjectTaskLineExists(int id)
        {
            return _context.ProjectTaskLine.Any(e => e.ProjectTaskId == id);
        }
    }
}