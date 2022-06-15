using TweetApp.Entites;
using TweetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Resources;

namespace TweetApp.Controllers
{
    [Route("api/User/")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _accountService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService accountService, IConfiguration configuration)
        {
            this._accountService = accountService;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegistration([FromBody] Users user)
        {
            try
            {
                string result = await this._accountService.UserRegistration(user);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string userName, string password)
        {
            try
            {
                var result = await this._accountService.Login(userName, password);
                string tkn = null;
                if (result != null)
                {
                    tkn = this.GenerateJwtToken(userName);
                }
                return this.Ok(new { token = tkn, user = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [HttpGet]
        [Route("forgot")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email, string newPassword)
        {
            try
            {
                var result = await this._accountService.ForgotPassword(email, newPassword);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [HttpGet]
        [Route("reset")]
        public async Task<IActionResult> ResetPassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                var result = await this._accountService.ResetPassword(email, oldPassword, newPassword);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await this._accountService.GetAllUsers();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest("Error Occurred. " + ex.Message);
            }
        }

        private string GenerateJwtToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Role, userName),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //recommended is 5 min
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this._configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                this._configuration["JwtIssuer"],
                this._configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
