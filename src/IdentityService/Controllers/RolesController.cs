using ExceptionHandler.ErrorModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/roles")]
    
    public class RolesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsersWithRoles()
        {
            try
            {
                var users = _userManager.Users.ToList();

                var usersWithRoles = users.Select(user => new RoleModel
                {
                    Email = user.Email,
                    Admin = _userManager.IsInRoleAsync(user, "Admin").Result.ToString(),
                    User = _userManager.IsInRoleAsync(user, "User").Result.ToString()
                }).ToList();

                return Ok(usersWithRoles);
            }
            catch (Exception ex)
            {

                return HandleException(ex, nameof(GetUsersWithRoles));
            }
           

            
        }
        
        [HttpPost("addrole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser([FromBody] RoleModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                // Remove existing roles
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                // Add new roles
                if (!string.IsNullOrEmpty(model.Admin) && model.Admin.ToLower() == "true")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                if (!string.IsNullOrEmpty(model.User) && model.User.ToLower() == "true")
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }

                return Ok(new { Message = "Roles added" });
            }
            catch (Exception ex)
            {

                return HandleException(ex, nameof(AddRoleToUser));
            }
            
        }
        private IActionResult HandleException(Exception ex, string methodName)
        {
            Console.WriteLine($"Exception in {methodName} method: {ex}");
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorDetails { Message = "Internal Server Error" + ex });
        }
    }
}
