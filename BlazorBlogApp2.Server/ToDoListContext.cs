using BlazorBlogApp2.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlogApp2.Server
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext() { }

        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoLists { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=BlogDb;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=blazor-admin;Password=admin!@#;Integrated Security=False");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasData(new Account
            {
                UserId = 1,
                Username = "admin",
                Password = "admin"
            });
        }
    }
}
