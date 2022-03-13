namespace RadioControlledDrivingSites.Server.DbContext;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shared;

public class DataContext : DbContext
{
    public DbSet<SiteDto> Sites { get; set; }

    private readonly string _connectionString;
    private readonly IConfiguration _configuration;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SiteDto>()
            .HasKey(x => x.Id);
    }
}
