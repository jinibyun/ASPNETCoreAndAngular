using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserForRegisterDto userFroRegisterDto)
        {
            // validate request
            userFroRegisterDto.UserName = userFroRegisterDto.UserName.ToLower();
            if(await _repo.UserExists(userFroRegisterDto.UserName))
                return BadRequest("Username already exist");
            var userToCreate = new User{
                Username = userFroRegisterDto.UserName
            };
            var createdUser = await _repo.Register(userToCreate, userFroRegisterDto.Password);
            return StatusCode(201);
        }
    }
}