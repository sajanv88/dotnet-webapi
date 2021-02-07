using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;
using dotnet5todoapp.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<TodoDto>> GetTodoItems()
        {
            var todoItems = this.repository.GetTodos();
            return Ok(todoItems);
        }

        // Get /todos/{id}
        [HttpGet("{id}")]
        public ActionResult<TodoDto> GetTodoItem(Guid id)
        {
            var todoItem = this.repository.GetTodo(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem.AsDto());
        }


        // Post /todos
        [HttpPost]
        public ActionResult<TodoDto> CreateTodoItem(CreateTodoDto todoDto)
        {
            TodoItem todoItem = new()
            {
                Id = Guid.NewGuid(),
                Description = todoDto.Description,
                CreatedAt = DateTimeOffset.UtcNow,
                Status = false
            };

            this.repository.CreateTodo(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { Id = todoItem.Id }, todoItem.AsDto());
        }

        // Put todos/{id}
        [HttpPut("{id}")]
        public ActionResult<TodoDto> UpdateTodoItem(Guid id, UpdateTodoDto todoDto)
        {
            var exitingTodoItem = this.repository.GetTodo(id);
            if (exitingTodoItem == null)
            {
                return NotFound();
            }

            TodoItem todoItem = exitingTodoItem with
            {
                Description = todoDto.Description,
                Status = todoDto.Status
            };

            this.repository.UpdateTodo(todoItem);
            return NoContent();
        }

        // Delete todos/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTodoItem(Guid id)
        {
            var exitingTodoItem = this.repository.GetTodo(id);
            if (exitingTodoItem == null)
            {
                return NotFound();
            }

            this.repository.DeleteTodo(id);
            return NoContent();
        }
    }
}