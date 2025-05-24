using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.DTOs;
using UMS.Application.Services;

namespace UMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _service.GetUserAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var user =  _service.GetAll();
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto dto)
        {
            await _service.CreateUserAsync(dto);
            return Created("", null);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDto dto)
        {
            await _service.UpdateUserAsync(id, dto);
            return NoContent();
        }

    }
}
