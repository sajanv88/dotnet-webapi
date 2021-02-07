using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;

namespace dotnet5todoapp.Repositories
{
    public class InMemoryRepository : ITodosRepository
    {
        private readonly List<TodoItem> todos = new()
        {
            new TodoItem { Id = Guid.NewGuid(), Description = "Learn dotnet core.", Status = false, CreatedAt = DateTimeOffset.UtcNow },
            new TodoItem { Id = Guid.NewGuid(), Description = "Build first todo app", Status = false, CreatedAt = DateTimeOffset.UtcNow }
        };

        public IEnumerable<TodoItem> GetTodos()
        {
            return todos;
        }

        public TodoItem GetTodo(Guid id)
        {
            return todos.Find(todo => todo.Id == id);
        }

        public void CreateTodo(TodoItem todoItem)
        {
            todos.Add(todoItem);
        }

        public void UpdateTodo(TodoItem todoItem)
        {
            var index = todos.FindIndex(todo => todo.Id == todoItem.Id);
            todos[index] = todoItem;
        }

        public void DeleteTodo(Guid id)
        {
            var index = todos.FindIndex(todo => todo.Id == id);
            todos.RemoveAt(index);
        }
    }
}
