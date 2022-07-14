
using BackendTraineesTask1.Models.Dto;
using BackendTraineesTask1.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTraineesTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "BigBoyUser")]

    public class UserController : ControllerBase
    {
        private readonly IUserService iuserService;
        public UserController( IUserService iuserService)
        {
            this.iuserService = iuserService;
        }
        [HttpPost("SendRequest")]
        public async Task<IActionResult> SendRequest(SendRequestDto requestDto)
        {
            var response = await iuserService.SendNotification(requestDto);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }

       
    }
}