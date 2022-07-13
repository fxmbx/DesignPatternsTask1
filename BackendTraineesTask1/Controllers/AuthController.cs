using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTraineesTask1.Data.Auth;
using BackendTraineesTask1.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackendTraineesTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo iauth;
        public AuthController(IAuthRepo iauth)
        {
            this.iauth = iauth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto req)
        {
                System.Console.WriteLine("hello");
            if (ModelState.IsValid)
            {
                System.Console.WriteLine("hello");
              var response = await iauth.Register(req);
              if(!response.Success){
                  return BadRequest(response);
              }
              return Ok(response);
            }
            return BadRequest(ModelState);

        }
        [HttpPost("Login")]
        public  async Task<IActionResult> Login(UserLoginDto req)
        {
             if (ModelState.IsValid)
            {
              var response = await iauth.Login(req);
              if(!response.Success){
                  return BadRequest(response);
              }
              return Ok(response);
            }
            return BadRequest(ModelState);
        }

       
    }
}