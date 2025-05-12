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
    
    
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<WagonType> WagonTypes { get; set; }
    public DbSet<Registrar> Registrars { get; set; }
    public DbSet<RailwayCistern> RailwayCisterns { get; set; }
    public DbSet<Vessel> Vessels { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<WheelPair> WheelPairs { get; set; }
    public DbSet<SideFrame> SideFrames { get; set; }
    public DbSet<Bolster> Bolsters { get; set; }
    public DbSet<CouplerModel> CouplerModels { get; set; }
    public DbSet<Coupler> Couplers { get; set; }
    public DbSet<ShockAbsorber> ShockAbsorbers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<PartInstallation> PartInstallations { get; set; }
    
    public DbSet<WagonModel> WagonModels { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("hello");
        
        builder.Entity<RailwayCistern>()
            .HasIndex(r => r.RegistrationNumber)
            .IsUnique();
            
        builder.Entity<Vessel>()
            .HasIndex(v => v.RailwayCisternsId)
            .IsUnique();
        
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
        
        builder.Entity<Location>()
            .HasIndex(l => l.Name)
            .IsUnique();
        
        builder.Entity<Location>()
            .HasMany(l => l.FromInstallations)
            .WithOne(p => p.FromLocation)
            .HasForeignKey(f => f.FromLocationId);
        
        builder.Entity<Location>()
            .HasMany(l => l.ToInstallations)
            .WithOne(p => p.ToLocation)
            .HasForeignKey(f => f.ToLocationId);
         
        
        builder.Entity<WagonModel>()
            .HasIndex(w => w.Name)
            .IsUnique();
        
        // Обновленная конфигурация для RailwayCistern
        builder.Entity<RailwayCistern>()
            .HasOne(rc => rc.WagonModel)
            .WithMany(wm => wm.RailwayCisterns)
            .HasForeignKey(rc => rc.ModelId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}