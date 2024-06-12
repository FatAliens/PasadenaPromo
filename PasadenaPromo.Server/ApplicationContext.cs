using PasadenaPromo.RepositoryItems;
using Microsoft.EntityFrameworkCore;

namespace PasadenaPromo;

public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserDbo> Users { get; set; } = null!;
    public DbSet<RoleDbo> Roles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserDbo>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .HasPrincipalKey(r => r.Id);
    }
}