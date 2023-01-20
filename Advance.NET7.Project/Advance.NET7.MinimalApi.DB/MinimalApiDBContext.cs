using Advance.NET7.MinimalApi.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Advance.NET7.MinimalApi.DB
{
    public class MinimalApiDBContext:DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Commodity>? Commodity { get; set; }
        public DbSet<Company>? Company { get; set; }
        public DbSet<SysUser>? SysUser { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=Anson-PC;Initial Catalog=MinimalApiData;User ID=sa;Password=Ansonyan168077");
            }
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }


        /// <summary>
        /// 配置对象和数据库表之间的映射关系
        /// </summary>
        /// <param name="modelBuilder"></param>
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