using System.Reflection;
using Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApiDbContext : DbContext
{
    public DbSet<Business> Businesses { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
}