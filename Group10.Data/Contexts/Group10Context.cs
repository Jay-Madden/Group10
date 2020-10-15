using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Group10.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Group10.Data.Contexts
{
    public class Group10Context : IdentityDbContext<AppUser>
    {
        public Group10Context(DbContextOptions<Group10Context> options) 
            : base(options)
        {
        }
        

        public DbSet<AppUser> AppUser { get; set; } = null!;
        public DbSet<Catalog> Catalogs { get; set; } = null!;
        public DbSet<Driver> Drivers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Sponsor> Sponsors { get; set; } = null!;
    }
}