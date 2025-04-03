using Microsoft.EntityFrameworkCore;

namespace ToDo.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options) { 
        }

        public DbSet<ToDoList> ToDoLists { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
