using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Entities;
using CourseConstructors.CourseConstructors.Core.Interfaces.Persistance;
using CourseConstructors.CourseConstructors.Core.Interfaces.Providers;
using Microsoft.EntityFrameworkCore;

namespace CourseConstructors.CourseConstructors.Infrastructure.Persistance;

public class Context : DbContext, IContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseUser> CourseToUsers { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<ContentItem> ContentItems { get; set; }
    
    public Context(DbContextOptions<Context> options, IDateTimeProvider dateTimeProvider) 
        :base(options)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseUser>()
            .HasKey(cu => new { cu.CourseId, cu.UserId });

        modelBuilder.Entity<CourseUser>()
            .HasOne(cu => cu.Course)
            .WithMany(c => c.Users)
            .HasForeignKey(cu => cu.CourseId);
        
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Lessons)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.ContentItems)
            .WithOne(ci => ci.Lesson)
            .HasForeignKey(ci => ci.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ContentItem>()
            .HasOne(p => p.Lesson)
            .WithMany(b => b.ContentItems)
            .HasForeignKey(p => p.LessonId);
    }
    
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            if (entry.State == EntityState.Added)
                entity.CreatedDate = _dateTimeProvider.GetCurrentTime();

            entity.EditDate = _dateTimeProvider.GetCurrentTime();
        }
    }
}