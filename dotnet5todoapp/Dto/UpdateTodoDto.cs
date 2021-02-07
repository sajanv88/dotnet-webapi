using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet5todoapp
{
    public record UpdateTodoDto
    {
        [Required]
        public String Description { get; init; }

        public Boolean Status { get; init; }
    }
}