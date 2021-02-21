using dotnet5todoapp.Repositories;
using dotnet5todoapp.Database;
using dotnet5todoapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace dotnet5todoapp
{
    public class PostgresDBTodoRepository : ITodosRepository
    {
        private readonly PostgesContext _context;

        public PostgresDBTodoRepository(PostgesContext context)
        {
            _context = context;
        }

        public async Task CreateTodoAsync(TodoItem todoItem)
        {
            await _context.todoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var entity = await _context.todoItems.FirstOrDefaultAsync(todoItem => todoItem.Id == id);
            _context.todoItems.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> GetTodoAsync(Guid id)
        {
            return await _context.todoItems.AsNoTracking().FirstOrDefaultAsync(todoItem => todoItem.Id == id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodosAsync()
        {
            return await _context.todoItems.ToListAsync();
        }

        public async Task UpdateTodoAsync(TodoItem todoItem)
        {
            _context.todoItems.Update(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}