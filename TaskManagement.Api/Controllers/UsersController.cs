using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Features.User.Commands.Login;
using TaskManagement.Application.Features.User.Commands.Register;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(userId);
        }

        // POST api/<UsersController>
        [HttpPost("login")]
        public async Task<IActionResult> Post(LoginCommand command)
        {
            var token = await _mediator.Send(command);
            
            HttpContext.Response.Cookies.Append("tasty-cookies", token);
            
            return Ok(token);
        }
    }
}
