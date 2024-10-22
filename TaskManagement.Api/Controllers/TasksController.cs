using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Features.Task.Commands.Create;
using TaskManagement.Application.Features.Task.Commands.Delete;
using TaskManagement.Application.Features.Task.Commands.Update;
using TaskManagement.Application.Features.Task.Queries.GetAllTasksByUserId;
using TaskManagement.Application.Features.Task.Queries.GetTaskDetails;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TasksController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
        
    // GET: api/<TasksController>
    [HttpGet]
    public async Task<ActionResult<List<TaskDto>>> Get()
    {
        var query = new GetAllTasksByUserIdQuery
        {
            UserId = GetUserId()
        };
            
        var tasks = await _mediator.Send(query);

        return tasks;
    }

    // GET api/<TasksController>/5
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDetailsDto>> Get(Guid id)
    {
        var query = new GetTaskDetailsQuery(id);

        var task = await _mediator.Send(query);
            
        return task;
    }

    // POST api/<TasksController>
    [HttpPost]
    public async Task<ActionResult> Create(CreateTaskCommand taskCommand)
    {
        taskCommand.UserId = GetUserId();
        var taskId = await _mediator.Send(taskCommand);
        return Ok(taskId);
    }
        

    // PUT api/<TasksController>/5
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(System.Guid id, UpdateTaskCommand taskCommand)
    {
        taskCommand.Id = id;
        taskCommand.UserId = GetUserId();
        await _mediator.Send(taskCommand);
        return NoContent();
    }
        

    // DELETE api/<TasksController>/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteTaskCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }

}