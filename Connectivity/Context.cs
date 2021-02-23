using System.Linq;
using MessengerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MessengerAPI.Connectivity
{
    public class Context : DbContext
    {
         public Context(DbContextOptions options)
          : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
       
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