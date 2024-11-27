using EntityPoste.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityPoste;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    private static readonly ILoggerFactory myFactory = LoggerFactory.Create(b =>
    {
        b.AddConsole();
        b.SetMinimumLevel(LogLevel.Information);
    });


    public DbSet<Address> Addresses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString =
            "Server=COMPUTER\\SQLEXPRESS;Database=PosteDB;TrustServerCertificate=True;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(myFactory);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
        base.OnModelCreating(modelBuilder);
    }
}