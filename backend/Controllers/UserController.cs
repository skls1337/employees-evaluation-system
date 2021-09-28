using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [EnableCors("AllowAllCorsPolicy")]
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }


        //@method   GET
        //@route    /api/users
        //@desc     GET All users from database
        
        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<User>> GetUsers() => await repository.GetUsers();
      
        //@method   GET
        //@route    /api/users/{id}
        //@desc     GET User with {id} from database
        [HttpGet]
        [Route("users/{id}")]
        public async Task<User> GetUser(string id) => await repository.GetUser(id);

        //@method   POST
        //@route    /api/login
        //@desc     GET Login to API and returns jwt
        [HttpPost]
        [Route("auth/login")]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            var token = await repository.Login(user.Email, user.Password);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        //@method   POST
        //@route    /api/register
        //@desc     GET Register to API and returns jwt
        [HttpPost]
        [Route("auth/register")]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            var token = await repository.Register(user.Email, user.Password);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}