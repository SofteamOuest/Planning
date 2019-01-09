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
    public class UserCredentialsController : ControllerBase
    {
        private readonly PlanningContext _context;

        public UserCredentialsController(PlanningContext context)
        {
            _context = context;
        }

		// GET: api/Users
		[HttpPost("Login")]
		public async Task<IActionResult> LoginUser([FromBody] dynamic credential)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			string username = credential.username;
			string password = credential.password;
			var userCredential = await _context.UserCredential.Include(uc => uc.User).ThenInclude(u => u.UserAccess).FirstOrDefaultAsync(uc => uc.User.Username == username && uc.Password == password);

			if (userCredential == null)
			{
				return NotFound();
			}

			return Ok(userCredential.User);
		}

		// GET: api/UserCredentials
		[HttpGet]
        public IEnumerable<UserCredential> GetUserCredential()
        {
            return _context.UserCredential;
        }

        // GET: api/UserCredentials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCredential([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCredential = await _context.UserCredential.FindAsync(id);

            if (userCredential == null)
            {
                return NotFound();
            }

            return Ok(userCredential);
        }

        // PUT: api/UserCredentials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCredential([FromRoute] int id, [FromBody] UserCredential userCredential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userCredential.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCredential).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCredentialExists(id))
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

        // POST: api/UserCredentials
        [HttpPost]
        public async Task<IActionResult> PostUserCredential([FromBody] UserCredential userCredential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserCredential.Add(userCredential);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCredential", new { id = userCredential.Id }, userCredential);
        }

        // DELETE: api/UserCredentials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCredential([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCredential = await _context.UserCredential.FindAsync(id);
            if (userCredential == null)
            {
                return NotFound();
            }

            _context.UserCredential.Remove(userCredential);
            await _context.SaveChangesAsync();

            return Ok(userCredential);
        }

        private bool UserCredentialExists(int id)
        {
            return _context.UserCredential.Any(e => e.Id == id);
        }
    }
}