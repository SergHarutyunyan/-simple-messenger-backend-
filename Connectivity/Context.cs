using System.Linq;
using simple_messenger_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace simple_messenger_backend.Connectivity
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
          : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<ChatChannel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
          
            base.OnModelCreating(modelbuilder);
        }
    }
}