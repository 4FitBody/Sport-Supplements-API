
using Microsoft.EntityFrameworkCore;
using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Infrastructure.Data;

public class SportSupplementDbContext : DbContext
{
    public DbSet<SportSupplement> SportSupplements { get; set; }

    public SportSupplementDbContext(DbContextOptions<SportSupplementDbContext> options) : base(options) { }
}
