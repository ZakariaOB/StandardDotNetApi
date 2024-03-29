﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StandardApi.Domain;

namespace StandardApi.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
