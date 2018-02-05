using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.EFCore
{
    /// <summary>
    /// Entity Framework DbContext setup
    /// </summary>
    public sealed class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Docs: https://msdn.microsoft.com/en-us/library/jj591620(v=vs.113).aspx
            // Example
            //modelBuilder.Entity<Course>() .HasMany(t => t.Instructors).WithMany(t => t.Courses)
        }
    }
}
