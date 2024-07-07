using Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.DataConfiguration;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Business> builder)
    {
        builder.HasKey(b => b.Id);
    }
}