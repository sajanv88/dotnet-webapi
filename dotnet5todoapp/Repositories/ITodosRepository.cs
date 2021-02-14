using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;
using System.Threading.Tasks;

namespace dotnet5todoapp.Repositories
{
    public interface ITodosRepository
    {
        Task<IEnumerable<TodoItem>> GetTodosAsync();
        Task<TodoItem> GetTodoAsync(Guid id);

        Task CreateTodoAsync(TodoItem todoItem);
        Task UpdateTodoAsync(TodoItem todoItem);

        Task DeleteTodoAsync(Guid id);
    }
}