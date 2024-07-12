using Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApiDbContext : DbContext
{
    public virtual DbSet<Business> Businesses { get; set; }

    public ApiDbContext() : base()
    {
    }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
}