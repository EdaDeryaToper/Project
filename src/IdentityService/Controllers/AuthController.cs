using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExceptionHandler.ErrorModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    return Ok(new { Message = "Registration successful" });
                }
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new ErrorDetails { Message = "Registration failed", Errors= errors});
            }
            catch (Exception ex)
            {

                return HandleException(ex, nameof(Register));
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);


                    var roleExists = await _roleManager.RoleExistsAsync("Admin");

                    if (!roleExists)
                    {

                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }


                    if (!await _userManager.IsInRoleAsync(user, "Admin") || !await _userManager.IsInRoleAsync(user, "User"))
                    {

                        await _userManager.AddToRoleAsync(user, "User");



                    }
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    return Ok(new { Bearer = token });

                }
                else
                {

                    return BadRequest(new { Message = "Login failed" });
                }


            }
            catch (Exception ex)
            {

                return HandleException(ex, nameof(Login));
            }
            
           
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Password reset successful" });
                }
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new ErrorDetails { Message = "Password reset failed", Errors = errors });
            }
            catch (Exception ex)
            {

                return HandleException(ex, nameof(ResetPassword));
            }
            
            
        }
        private IActionResult HandleException(Exception ex, string methodName)
        {
            Console.WriteLine($"Exception in {methodName} method: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDetails { Message = "Internal Server Error" + ex });
        }


    }
}
