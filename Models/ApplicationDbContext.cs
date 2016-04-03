using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Floodlight.Service.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<DbBackground> Backgrounds { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder
                .Entity<DbBackground>()
                .HasIndex(b => b.Id)
                .IsUnique(true);
                
            builder
                .Entity<ApplicationUser>()
                .HasIndex(b => b.Guid)
                .IsUnique(true);
        }
    }
}
