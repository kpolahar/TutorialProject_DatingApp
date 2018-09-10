using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // Validate request

            // Convert username to lowercase
            //   Ensure username is unique regardless of case
            username = username.ToLower();

            // Use repository to check if user exists
            if (await _repo.UserExists(username))
                return BadRequest("That username has already been taken.");

            // Create the new User object
            var userToCreate = new User { Username = username };

            // Use repository to create the new User
            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}