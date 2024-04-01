using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonsaiBackend.Models {
    public class DBBonsaiContext : DbContext {
        public DBBonsaiContext(DbContextOptions<DBBonsaiContext> options) : base(options){

        }
        public DbSet<Users> User {get; set;}
        public DbSet<Tasks> Task {get; set;}
        public DbSet<Clients> Client {get; set;}
        public DbSet<Payments> Payment {get; set;}
        public DbSet<TaskTimes> TaskTime {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>()
                .HasOne(c => c.Users)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TaskTimes>()
                .HasOne(c => c.Users)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payments>()
                .HasOne(c => c.Users)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payments>()
                .HasOne(c => c.TaskTimes)
                .WithMany()
                .HasForeignKey(x => x.TimeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payments>()
                .HasOne(c => c.Tasks)
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            Console.WriteLine(configuration.GetConnectionString("DBBonsaiDatabase"));
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBBonsaiDatabase"));
        }
        */
    }
}