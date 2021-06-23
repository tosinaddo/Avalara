using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalara.Models.Contracts.Response;
using Avalara.Models.Contracts.Request;
using Avalara.Interfaces;
using Avalara.Common;
using System.Net.Mime;

namespace Avalara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _serv;
        public TodoController(ITodoService service)
        {
            _serv = service;
        }

        [HttpGet]
        [Route("todos")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(List<TodoItem>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodos()
        {
            var serviceResponse = await _serv.GetTodos();
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)NotFound(serviceResponse.ErrorDetails);
        }
        [HttpGet]
        [Route("todos/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItem))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodoItem([FromRoute]Guid id)
        {
            var serviceResponse = await _serv.GetTodoItem(id);
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)NotFound(serviceResponse.ErrorDetails);
        }
        [HttpPost]
        [Route("todos")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<TodoItem>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTodoItems(AddTodo todo)
        {
            var serviceResponse = await _serv.AddTodoItems(todo);
            return serviceResponse.Success ? Created(string.Empty, serviceResponse.ResponseData) : (IActionResult)Conflict(serviceResponse.ErrorDetails);


        }
        [HttpPut]
        [Route("todos")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TodoItem>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodos(UpdateTodo todo)
        {
            var serviceResponse = await _serv.UpdateTodos(todo);
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)Conflict(serviceResponse.ErrorDetails);


        }
        [HttpDelete]
        [Route("todos")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTodos(DeleteTodo todo)
        {
            var serviceResponse = await _serv.DeleteTodos(todo);
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)NotFound(serviceResponse.ErrorDetails);


        }

        [HttpGet]
        [Route("tasks")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskItem>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTasks()
        {
            var serviceResponse = await _serv.GetTasks();
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)NotFound(serviceResponse.ErrorDetails);
        }
        [HttpGet]
        [Route("tasks/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskItem))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTaskItem([FromRoute] int id)
        {
            var serviceResponse = await _serv.GetTaskItem(id);
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)NotFound(serviceResponse.ErrorDetails);
        }
        [HttpPost]
        [Route("tasks")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<TaskItem>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTasks(AddTask task)
        {
            var serviceResponse = await _serv.AddTasks(task);
            return serviceResponse.Success ? Created(string.Empty, serviceResponse.ResponseData) : (IActionResult)Conflict(serviceResponse.ErrorDetails);


        }
        [HttpPut]
        [Route("tasks")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskItem>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTasks(UpdateTask task)
        {
            var serviceResponse = await _serv.UpdateTasks(task);
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)Conflict(serviceResponse.ErrorDetails);


        }
        [HttpDelete]
        [Route("tasks")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessDetails))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTasks(DeleteTask task)
        {
            var serviceResponse = await _serv.DeleteTasks(task);
            return serviceResponse.Success ? Ok(serviceResponse.ResponseData) : (IActionResult)NotFound(serviceResponse.ErrorDetails);


        }
    }
}
