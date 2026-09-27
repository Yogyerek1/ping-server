using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ping.Server.Features.Authentication.Models;

namespace Ping.Server.Data;

public class PingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public PingDbContext(DbContextOptions<PingDbContext> options) : base(options) {}

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(token => token.Id);

            entity.HasIndex(token => token.TokenHash)
                .IsUnique();
            
            entity.HasIndex(token => token.UserId);

            entity.HasOne(token => token.User)
                .WithMany()
                .HasForeignKey(token => token.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}