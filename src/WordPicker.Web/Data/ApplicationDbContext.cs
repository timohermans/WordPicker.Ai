using Microsoft.EntityFrameworkCore;
using WordPicker.Web.Models;

namespace WordPicker.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WordList> WordLists => Set<WordList>();

    public DbSet<WordEntry> WordEntries => Set<WordEntry>();

    public DbSet<PickHistory> PickHistories => Set<PickHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WordList>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<WordEntry>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.LastPickCycle)
                .HasDefaultValue(0);

            entity.HasOne(e => e.WordList)
                .WithMany(w => w.Words)
                .HasForeignKey(e => e.WordListId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.WordListId, e.Value })
                .IsUnique();
        });

        modelBuilder.Entity<PickHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.PickedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(e => e.WordEntry)
                .WithMany(w => w.PickHistories)
                .HasForeignKey(e => e.WordEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.WordList)
                .WithMany(w => w.PickHistories)
                .HasForeignKey(e => e.WordListId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.WordListId);
            entity.HasIndex(e => e.WordEntryId);
        });
    }
}
