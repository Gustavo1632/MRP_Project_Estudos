using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MRP.Models.Entities;
using MRP.Models.ValueObjects;

namespace MRP.Data
{
    public class MRPContext : DbContext
    {
        public MRPContext(DbContextOptions<MRPContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<MovimentRecord> MovimentsRecord { get; set; }
        public DbSet<CNPJ> CNPJ { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
        }

    }
}
