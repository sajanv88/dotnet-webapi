using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet5todoapp
{
    public record CreateTodoDto
    {
        [Required]
        public String Description { get; init; }
    }
}