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
    public class ProjectTasksController : ControllerBase
    {
        private readonly PlanningContext _context;

        public ProjectTasksController(PlanningContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTasks
        [HttpGet]
        public IEnumerable<ProjectTask> GetProjectTask()
        {
            return _context.ProjectTask;
        }

        // GET: api/ProjectTasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTask = await _context.ProjectTask.FindAsync(id);

            if (projectTask == null)
            {
                return NotFound();
            }

            return Ok(projectTask);
        }

        // PUT: api/ProjectTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask([FromRoute] int id, [FromBody] ProjectTask projectTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(id))
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

        // POST: api/ProjectTasks
        [HttpPost]
        public async Task<IActionResult> PostProjectTask([FromBody] ProjectTask projectTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectTask.Add(projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTask", new { id = projectTask.Id }, projectTask);
        }

        // DELETE: api/ProjectTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTask = await _context.ProjectTask.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            _context.ProjectTask.Remove(projectTask);
            await _context.SaveChangesAsync();

            return Ok(projectTask);
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTask.Any(e => e.Id == id);
        }
    }
}