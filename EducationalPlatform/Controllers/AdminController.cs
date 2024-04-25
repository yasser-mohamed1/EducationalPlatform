using EducationalPlatform.Data;
using EducationalPlatform.DTO;
using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly EduPlatformContext context;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> _userManager, EduPlatformContext _context)
        {
            this.roleManager = roleManager;
            userManager = _userManager;
            context = _context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registeration(RegisterAdminDto adminDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new();
                user.UserName = adminDto.Username;
                user.Email = adminDto.Email;
                user.PhoneNumber = adminDto.Phone;
                IdentityResult result = await userManager.CreateAsync(user, adminDto.Password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return Ok("Admin Registeration Success");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> CreateRole(string _role)
        {
            var role = new IdentityRole(_role);
            IdentityResult result = await roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                return Ok("Role Created Successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
