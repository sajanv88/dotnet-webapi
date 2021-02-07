using dotnet5todoapp.Models;

namespace dotnet5todoapp
{
    public static class Extension
    {
        public static TodoDto AsDto(this TodoItem todoItem)
        {
            return new TodoDto
            {
                Id = todoItem.Id,
                Description = todoItem.Description,
                Status = todoItem.Status,
                CreatedAt = todoItem.CreatedAt
            };
        }
    }
}