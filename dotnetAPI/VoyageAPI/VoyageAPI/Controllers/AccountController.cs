using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VoyageAPI.Data;
using VoyageAPI.Models;
using VoyageAPI.Models.DTOs.Request;
using VoyageAPI.Options;

namespace VoyageAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly JwtSettings _jwtSettings;

        public AccountController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, JwtSettings jwtSettings)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model State");
            }

            var user = new User
            {
                Email = userRequest.Email,
                UserName = userRequest.Email

                
            };
            
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser!=null)
            {
                return BadRequest("E-mail already taken");
            }

           var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return BadRequest(errors);
            }

            return Ok("Registered Successfully");
        }




        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInRequest userSignInRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model State");
            }

            var user = await _userManager.FindByNameAsync(userSignInRequest.UserName);
            if (user == null)
            {
                return BadRequest("Bad credentials");
            }
            
           var result =  await _userManager.CheckPasswordAsync(user, userSignInRequest.Password);

            if (!result)
            {
                return BadRequest("Bad credentials");
            }

            //Generate a JWT

            var tokenResponse =  await GenerateToken(user);
            return Ok(tokenResponse);

          
        }

     
        private async Task<object> GenerateToken(User user)
        {

            // Rajouter les claims du user (role,id)
            List<Claim> authClaims = new List<Claim>();

            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));






            SymmetricSecurityKey authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://locahost:44355",
                audience: "http://localhost:4200",
                claims: authClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256)
                );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                validTo = token.ValidTo
            };


        }
    }

}
