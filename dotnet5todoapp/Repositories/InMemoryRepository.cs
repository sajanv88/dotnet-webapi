using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;
using System.Threading.Tasks;

namespace dotnet5todoapp.Repositories
{
    public class InMemoryRepository : ITodosRepository
    {
        private readonly List<TodoItem> todos = new()
        {
            new TodoItem { Id = Guid.NewGuid(), Description = "Learn dotnet core.", Status = false, CreatedAt = DateTimeOffset.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Description = "Build first todo app", Status = false, CreatedAt = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<TodoItem>> GetTodosAsync()
        {
            return await Task.FromResult(todos);
        }

        public async Task<TodoItem> GetTodoAsync(Guid id)
        {
            return await Task.FromResult(todos.Find(todo => todo.Id == id));
        }

        public async Task CreateTodoAsync(TodoItem todoItem)
        {
            todos.Add(todoItem);
            await Task.CompletedTask;
        }

        public async Task UpdateTodoAsync(TodoItem todoItem)
        {
            var index = todos.FindIndex(todo => todo.Id == todoItem.Id);
            todos[index] = todoItem;
            await Task.CompletedTask;

        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var index = todos.FindIndex(todo => todo.Id == id);
            todos.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
