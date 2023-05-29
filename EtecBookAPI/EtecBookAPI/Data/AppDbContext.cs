using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtecBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EtecBookAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}