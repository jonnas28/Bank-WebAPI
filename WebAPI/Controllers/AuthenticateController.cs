using Bank.Common.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Models;
using WebAPI.Repository.Identity;
using WebAPI.Response;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private UserManager<User> _userManager;
        private IAuthenticateRepository _authenticate;
        private IConfiguration _configuration;
        public AuthenticateController(UserManager<User> userManager, IAuthenticateRepository authenticate, IConfiguration configuration)
        {
            _userManager = userManager;
            _authenticate = authenticate;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        [SwaggerOperation(
            Summary = "Register a new user",
            Description = "Create a new user."
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null) return StatusCode(403, new ApiResponse(409, "User already exists!"));
            var userEmailExists = await _userManager.FindByNameAsync(model.Username);
            if (userEmailExists != null) return StatusCode(403, new ApiResponse(409, "User email exists!"));

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(500, JsonConvert.SerializeObject(result.Errors)));
            return Ok(new ApiResponse(204,"Registration Success"));
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(
            Summary = "Login for existing user",
            Description = "Authenticate an existing user and generate an authentication token."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await _authenticate.Login(user);
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                await _userManager.UpdateAsync(user);

                var ret = new TokenDTO
                {
                    expiration = token.ValidTo,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                };

                return Ok(ret);
            }
            return Unauthorized(new ApiResponse(401));
        }
    }
}
