using System;
using GridCom.Models;
using Microsoft.EntityFrameworkCore;

namespace GridCom.Data
{
    public class GridContext : DbContext
    {
        public GridContext(DbContextOptions<GridContext> options) : base(options)
        {
        }
            public DbSet<CasesOfOutage> CasesOfOutage { get; set; }
            public DbSet<RES> RES { get; set; }
            public DbSet<Substation> Substations { get; set; }
            public DbSet<Feeder> Feeders { get; set; }
            public DbSet<Outage> Outages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CasesOfOutage>().ToTable("CasesOfOutage");
            modelBuilder.Entity<RES>().ToTable("RES");
            modelBuilder.Entity<Substation>().ToTable("Substation");
            modelBuilder.Entity<Feeder>().ToTable("Feeder");
            modelBuilder.Entity<Outage>().ToTable("Outage");
           
        }
    }
}

