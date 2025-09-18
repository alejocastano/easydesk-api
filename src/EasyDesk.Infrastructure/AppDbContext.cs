using Microsoft.EntityFrameworkCore;
using EasyDesk.Domain;

namespace EasyDesk.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<TicketPriority> TicketPriorities { get; set; }
    public DbSet<TicketAudit> TicketAudits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Autoincrement for Ids
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Role>()
            .Property(r => r.Id)
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

        modelBuilder.Entity<TicketPriority>()
            .Property(tp => tp.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<TicketAudit>()
            .Property(ta => ta.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedByUser)
            .WithMany(u => u.CreatedTickets)
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedToUser)
            .WithMany(u => u.AssignedTickets)
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
