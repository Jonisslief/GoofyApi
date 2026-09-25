using APL.DTOs;
using APL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _token;

        public UsersController(IUserService userService, IPasswordService passwordService, ITokenService tokenService)
        {
            _userService = userService;
            _passwordService = passwordService;
            _token = tokenService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userClaims = HttpContext.User.Claims;
            var userId = userClaims.First(c => c.Type == "UserID").Value;
            var task = _userService.GetUserByIdAsync(int.Parse(userId));

            var users = await task;
            if (task.IsCompletedSuccessfully)
            {
                if (users == null) { return NotFound(); }
                return Ok(users);
            }
            return BadRequest(task.Exception);
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var task = _userService.GetAllAsync();
            var users = await task;
            if (task.IsCompletedSuccessfully)
            {
                if (users == null) { return NotFound(); }
                return Ok(users);
            }
            return BadRequest(task.Exception);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            var task = _userService.GetUserByIdAsync(id);
            var user = await task;
            if (task.IsCompletedSuccessfully)
            {
                if (user == null) { return NotFound(); }
                return Ok(user);
            }
            return BadRequest(task.Exception);
        }

        // POST api/<UsersController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dTO)
        {
            if (await _userService.AddUserAsync(dTO))
            {
                return Created();
            };
            return BadRequest("It seems someone is already using that name and or Email");
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = _userService.DeleteUserAsync(id);

            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(task.Exception);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO dTO)
        {
            var user = await _userService.GetUserByNameAsync(dTO.Username);

            if (user != null && await _passwordService.VerifyHashedPasswordAsync(user, dTO.Password) == PasswordVerificationResult.Success)
            {
                var token = await _token.GenerateToken(user!);
                return Ok(new { token });
            }
            return BadRequest(new { message = "Username or password is incorrect"});
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UserDTO dTO)
        {
            var task = _userService.UpdateUser(dTO);
            await task;
            if (task.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(task);
        }
    }
}
