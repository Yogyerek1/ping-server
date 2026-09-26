using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ping.Server.Features.Authentication.Models;

namespace Ping.Server.Data;

public class PingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public PingDbContext(DbContextOptions<PingDbContext> options) : base(options) {}
}