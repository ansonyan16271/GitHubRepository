using Advance.NET7.MinimalApi.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Advance.NET7.MinimalApi.DB
{
    public class MinimalApiDbContext:DbContext
    {
        public static readonly ILoggerFactory MyloggerFactory = LoggerFactory.Create(builder => { builder.AddConsole();});

        public DbSet<Commodity>? Commodity { get; set; }
        public DbSet<Company>? Company { get; set; }
        public DbSet<SysUser>? SysUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=Anson-PC;Initial Catalog=MinimalApiData;User ID=sa;Password=Ansonyan168077;Trust Server Certificate=true");
            }
            optionsBuilder.UseLoggerFactory(MyloggerFactory);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commodity>()
                .ToTable("Commodity")
                .Property(c => c.Title)
                .HasColumnName("Title");
            base.OnModelCreating(modelBuilder);
        }
    }
}