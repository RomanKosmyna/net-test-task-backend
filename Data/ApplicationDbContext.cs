﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using net_test_task_backend.Models;

namespace net_test_task_backend.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Url> Urls { get; set; }
    public DbSet<About> Abouts { get; set; }

    public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
    }
}