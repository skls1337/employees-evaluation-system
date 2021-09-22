using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            var result = await repository.Login(user.Email, user.Password);

            if (result is null)
            {
                return Unauthorized();
            }

            return Ok(new { result });
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            var result = await repository.Register(user.Email, user.Password);

            if (result is null)
            {
                return Unauthorized();
            }

            return Ok(new { result });
        }
    }
}