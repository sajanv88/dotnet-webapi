using System;

namespace dotnet5todoapp
{
    public record TodoDto
    {
        public Guid Id { get; init; }
        public String Description { get; init; }
        public Boolean Status { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
    }
}