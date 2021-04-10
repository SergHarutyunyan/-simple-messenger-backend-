using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using simple_messenger_backend.Models;

namespace simple_messenger_backend.Connectivity
{
    public class MySQLDataManager : DbContext
    {
        public MySQLDataManager(DbContextOptions options)
          : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            base.OnModelCreating(modelBuilder);
        }
    }
}