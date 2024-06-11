using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess
{
    public class SMDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=DESKTOP-CFE50DT\\SQLEXPRESS; Database=SMS; User Id=sa; Password=admin";
            if (optionsBuilder != null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                 .HasKey(k => k.Username);

            modelBuilder.Entity<Student>()
                .HasKey(k => k.Id);
        }
    }
}
