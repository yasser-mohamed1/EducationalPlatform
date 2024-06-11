using EducationalPlatform.DTO;
using EducationalPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationalPlatform.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterAdminDto adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _adminService.RegisterAdminAsync(adminDto);
            if (result != "Admin Registration Success")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> CreateRole(string role)
        {
            var result = await _adminService.CreateRoleAsync(role);
            if (result != "Role Created Successfully")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
