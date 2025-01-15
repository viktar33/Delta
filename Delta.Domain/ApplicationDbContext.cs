using Delta.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Delta.Domain;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Sale> Sales { get; set; }
}