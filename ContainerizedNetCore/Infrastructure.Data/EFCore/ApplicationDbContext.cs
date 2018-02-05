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
        //public DbSet<MyEntity> MyEntities { get; set; }
    }
}
