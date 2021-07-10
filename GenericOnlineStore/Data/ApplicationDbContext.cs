using GenericOnlineStore.Models.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericOnlineStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlphaUser>();

            base.OnModelCreating(builder);
        }

        public DbSet<AlphaUser> users { get; set; }
        public DbSet<APurchase> purchases { get; set; }
        public DbSet<ATransaction> transactions { get; set; }
        public DbSet<BaseAccount> accounts { get; set; }
        public DbSet<BaseItem> items { get; set; }
        public DbSet<Inventory> inventory { get; set; }

    }
}
