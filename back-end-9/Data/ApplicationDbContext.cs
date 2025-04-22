using back_end_9.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<FileModel> Files { get; set; }
    public DbSet<ShortUrl> ShortUrls { get; set; }
    public DbSet<UserFile> UserFiles { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("hello");
        
        builder.Entity<FileModel>()
            .HasKey(f => f.FileId);

        builder.Entity<ShortUrl>()
            .HasKey(su => su.ShortUrlId);

        builder.Entity<UserFile>()
            .HasKey(uf => uf.UserFileId);

        // Настройка отношений между FileModel и ShortUrl
        builder.Entity<ShortUrl>()
            .HasOne(su => su.File)
            .WithMany(f => f.ShortUrls)
            .HasForeignKey(su => su.FileId);

        // Настройка отношений между User и UserFile
        builder.Entity<UserFile>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.UserFiles)
            .HasForeignKey(uf => uf.UserId);
    }
}