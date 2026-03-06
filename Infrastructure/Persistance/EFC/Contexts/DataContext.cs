using Infrastructure.Persistance.EFC.Configurations;
using Infrastructure.Persistance.EFC.Enteties;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.EFC.Contexts;

public sealed class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<MemberEntity> Members => Set<MemberEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberEntityConfiguration());
    }
}
