using JwtAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;


        public AuthController(UserManager<IdentityUser> userManager, IConfiguration config, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }


        //Register Method for JWT Authentication in simple project
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        return Ok(model);
                    }

                    return BadRequest(result.Errors);
                }
                return BadRequest(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //Login Method for JWT Authentication

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            //// Log the incoming data to make sure it's being received correctly
            //Console.WriteLine($"Username: {login.UserName}, Password: {login.Password}");

            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return Unauthorized();
            }

            var loginResult = await _userManager.CheckPasswordAsync(user, login.Password);
            if (loginResult)
            {
                //Auth claims used in JWT authentication
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@1234"));
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:4200",
                    audience: "http://localhost:4200",
                    expires: DateTime.Now.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            Console.WriteLine("Invalid credentials");
            return Unauthorized();
        }

    }
}