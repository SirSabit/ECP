using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DbContexts.Implementation
{
    public class PostgresDbContext(DbContextOptions options, IConfiguration configuration) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreCs"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderLogEntity>().ToTable("orderlogs");
            modelBuilder.Entity<OrderLogEntity>().Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<OrderLogEntity>().Property(p => p.Success).HasColumnName("success");
            modelBuilder.Entity<OrderLogEntity>().Property(p => p.Message).HasColumnName("message");
            modelBuilder.Entity<OrderLogEntity>().Property(p => p.ResponseData).HasColumnName("responsedata");
            modelBuilder.Entity<OrderLogEntity>().Property(p => p.LogDate).HasColumnName("logdate");

        }
        public virtual DbSet<OrderLogEntity> OrderLogs { get; set; }
    }
}
