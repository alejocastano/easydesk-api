using Microsoft.EntityFrameworkCore;
using EasyDesk.Domain;

namespace EasyDesk.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Autoincrement for Ids
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<UserRole>()
            .Property(ur => ur.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Ticket>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd(); 
        
        modelBuilder.Entity<TicketStatus>()
            .Property(ts => ts.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TicketComment>()
            .Property(tc => tc.Id)
            .ValueGeneratedOnAdd();

    }
}
