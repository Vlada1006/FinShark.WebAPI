using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(u => u.HasKey(x => new { x.AppUserId, x.StockId }));

            builder.Entity<Portfolio>()
             .HasOne(u => u.AppUser)
             .WithMany(u => u.Portfolios)
             .HasForeignKey(x => x.AppUserId);

            builder.Entity<Portfolio>()
             .HasOne(u => u.Stock)
             .WithMany(u => u.Portfolios)
             .HasForeignKey(x => x.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}