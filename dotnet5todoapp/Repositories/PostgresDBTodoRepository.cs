using dotnet5todoapp.Repositories;
using dotnet5todoapp.Database;
using dotnet5todoapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dotnet5todoapp
{
    public class PostgresDBTodoRepository : ITodosRepository
    {
        private readonly PostgesContext _context;

        public PostgresDBTodoRepository(PostgesContext context)
        {
            _context = context;
        }

        public void CreateTodo(TodoItem todoItem)
        {
            _context.todoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public void DeleteTodo(Guid id)
        {
            var entity = _context.todoItems.FirstOrDefault(todoItem => todoItem.Id == id);
            _context.todoItems.Remove(entity);
            _context.SaveChanges();
        }

        public TodoItem GetTodo(Guid id)
        {
            return _context.todoItems.AsNoTracking().FirstOrDefault(todoItem => todoItem.Id == id);
        }

        public IEnumerable<TodoItem> GetTodos()
        {
            return _context.todoItems.ToList();
        }

        public void UpdateTodo(TodoItem todoItem)
        {
            _context.todoItems.Update(todoItem);
            _context.SaveChanges();
        }
    }
}