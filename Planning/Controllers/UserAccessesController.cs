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
    public class UserAccessesController : ControllerBase
    {
        private readonly PlanningContext _context;

        public UserAccessesController(PlanningContext context)
        {
            _context = context;
        }

        // GET: api/UserAccesses
        [HttpGet]
        public IEnumerable<UserAccess> GetUserAccess()
        {
            return _context.UserAccess;
        }

        // GET: api/UserAccesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAccess([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAccess = await _context.UserAccess.FindAsync(id);

            if (userAccess == null)
            {
                return NotFound();
            }

            return Ok(userAccess);
        }

        // PUT: api/UserAccesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccess([FromRoute] int id, [FromBody] UserAccess userAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAccess.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccessExists(id))
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

        // POST: api/UserAccesses
        [HttpPost]
        public async Task<IActionResult> PostUserAccess([FromBody] UserAccess userAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserAccess.Add(userAccess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAccess", new { id = userAccess.Id }, userAccess);
        }

        // DELETE: api/UserAccesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccess([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAccess = await _context.UserAccess.FindAsync(id);
            if (userAccess == null)
            {
                return NotFound();
            }

            _context.UserAccess.Remove(userAccess);
            await _context.SaveChangesAsync();

            return Ok(userAccess);
        }

        private bool UserAccessExists(int id)
        {
            return _context.UserAccess.Any(e => e.Id == id);
        }
    }
}