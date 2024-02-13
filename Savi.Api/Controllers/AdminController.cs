using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savi.Core.DTO;
using Savi.Core.IServices;
using Savi.Core.Services;
using Savi.Model;
using Savi.Utility.Pagination;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("groupId")]
        public IActionResult GetGroupSaving(string groupId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid model state.", StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            }
            return Ok(_adminService.GetGroupSavingById(groupId));
        }

        [HttpGet("allGroupSavingsToday")]
        public async Task<IActionResult> GetAllGroupSavingsToday(int perPage, int page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid model state.", StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            }
            return Ok(await _adminService.GetAllGroupSavingsTodayAsync(perPage, page));
        }
    }
}
