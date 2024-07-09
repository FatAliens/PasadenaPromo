using PasadenaPromo.RepositoryItems;
using Microsoft.EntityFrameworkCore;
using PasadenaPromo.Server.RepositoryItems;

namespace PasadenaPromo;

public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserDbo> Users { get; set; } = null!;
    public DbSet<RoleDbo> Roles { get; set; } = null!;
    public DbSet<EmployeeDbo> Employees { get; set; } = null!;
    public DbSet<SpecializationDbo> Specializations { get; set; } = null!;
    public DbSet<ServiceDbo> Services { get; set; } = null!;
    public DbSet<PortfolioItemDbo> PortfolioItems { get; set; } = null!;
    public DbSet<WorkingWeekDayDbo> WorkingWeekDays { get; set; } = null!;
    public DbSet<WorkingWeekDbo> WorkingWeeks { get; set; } = null!;
    public DbSet<DayOfWeekDbo> DaysOfWeek { get; set; } = null!;
    public DbSet<WorkingDayDbo> WorkingDays { get; set; } = null!;
    public DbSet<WorkingPeriodDbo> WorkingPeriods { get; set; } = null!;
    public DbSet<ReservationDbo> Reservations { get; set; } = null!;
    public DbSet<ReservationStatusDbo> ReservationStatuses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserDbo>()
            .HasOne(u => u.Role);
        modelBuilder
            .Entity<WorkingDayDbo>()
            .HasMany(d => d.WorkingPeriods)
            .WithMany(p => p.WorkingDays)
            .UsingEntity(j => j.ToTable("WorkingDayPeriods"));
    }
}