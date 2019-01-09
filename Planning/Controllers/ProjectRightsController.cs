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
    public class ProjectRightsController : ControllerBase
    {
        private readonly PlanningContext _context;

        public ProjectRightsController(PlanningContext context)
        {
            _context = context;
        }

        // GET: api/ProjectRights
        [HttpGet]
        public IEnumerable<ProjectRight> GetProjectRight()
        {
            return _context.ProjectRight;
        }

        // GET: api/ProjectRights/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectRight([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectRight = await _context.ProjectRight.FindAsync(id);

            if (projectRight == null)
            {
                return NotFound();
            }

            return Ok(projectRight);
        }

        // PUT: api/ProjectRights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectRight([FromRoute] int id, [FromBody] ProjectRight projectRight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectRight.ProjectTaskId)
            {
                return BadRequest();
            }

            _context.Entry(projectRight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectRightExists(id))
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

        // POST: api/ProjectRights
        [HttpPost]
        public async Task<IActionResult> PostProjectRight([FromBody] ProjectRight projectRight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectRight.Add(projectRight);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectRightExists(projectRight.ProjectTaskId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjectRight", new { id = projectRight.ProjectTaskId }, projectRight);
        }

        // DELETE: api/ProjectRights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectRight([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectRight = await _context.ProjectRight.FindAsync(id);
            if (projectRight == null)
            {
                return NotFound();
            }

            _context.ProjectRight.Remove(projectRight);
            await _context.SaveChangesAsync();

            return Ok(projectRight);
        }

        private bool ProjectRightExists(int id)
        {
            return _context.ProjectRight.Any(e => e.ProjectTaskId == id);
        }
    }
}