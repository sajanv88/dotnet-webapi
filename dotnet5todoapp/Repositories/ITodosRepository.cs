using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;

namespace dotnet5todoapp.Repositories
{
    public interface ITodosRepository
    {
        IEnumerable<TodoItem> GetTodos();
        TodoItem GetTodo(Guid id);

        void CreateTodo(TodoItem todoItem);
        void UpdateTodo(TodoItem todoItem);

        void DeleteTodo(Guid id);
    }
}