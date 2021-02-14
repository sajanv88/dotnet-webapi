using dotnet5todoapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet5todoapp.Database
{
    public class PostgesContext : DbContext
    {
        public PostgesContext(DbContextOptions<PostgesContext> options) : base(options)
        {

        }
        public DbSet<TodoItem> todoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

    }
}
