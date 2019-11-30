using Microsoft.EntityFrameworkCore;
using System;
using TwitterAPI.Models;

namespace TwitterAPI.Contexts
{
    public class HistoricalHashtagContext : DbContext
    {
        public HistoricalHashtagContext(DbContextOptions<HistoricalHashtagContext> options): base(options)
        {
        }

        public DbSet<HistoricalHashtag> HistoricalHashtags { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricalHashtag>().ToTable("HistoricalHashtags");
        }
    }
}
