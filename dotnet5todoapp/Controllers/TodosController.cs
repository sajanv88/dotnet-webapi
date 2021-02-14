using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;
using dotnet5todoapp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnet5todoapp.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodosController : ControllerBase
    {
        private readonly ITodosRepository repository;
        public TodosController(ITodosRepository repository)
        {
            this.repository = repository;
        }

        // Get /todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodoItemsAsync()
        {
            var todoItems = await this.repository.GetTodosAsync();
            return Ok(todoItems);
        }

        // Get /todos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetTodoItemAsync(Guid id)
        {
            var todoItem = await this.repository.GetTodoAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem.AsDto());
        }


        // Post /todos
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateTodoItemAsync(CreateTodoDto todoDto)
        {
            TodoItem todoItem = new()
            {
                Id = Guid.NewGuid(),
                Description = todoDto.Description,
                CreatedAt = DateTimeOffset.UtcNow,
                Status = false
            };

            await this.repository.CreateTodoAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItemAsync), new { Id = todoItem.Id }, todoItem.AsDto());
        }

        // Put todos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> UpdateTodoItemAsync(Guid id, UpdateTodoDto todoDto)
        {
            var exitingTodoItem = await this.repository.GetTodoAsync(id);
            if (exitingTodoItem == null)
            {
                return NotFound();
            }

            TodoItem todoItem = exitingTodoItem with
            {
                Description = todoDto.Description,
                Status = todoDto.Status
            };

            await this.repository.UpdateTodoAsync(todoItem);
            return NoContent();
        }

        // Delete todos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItemAsync(Guid id)
        {
            var exitingTodoItem = await this.repository.GetTodoAsync(id);
            if (exitingTodoItem == null)
            {
                return NotFound();
            }

            await this.repository.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}