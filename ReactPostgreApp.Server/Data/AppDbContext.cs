using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReactPostgreApp.Server.Data;

namespace PostgreSQL.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chỉ định khóa chính cho entity WeatherForecast
            modelBuilder.Entity<WeatherForecast>()
                .HasKey(wf => wf.Date); // Date là khóa chính

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    }
}