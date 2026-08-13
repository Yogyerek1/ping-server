using Microsoft.EntityFrameworkCore;

namespace Ping.Server.Data;

public class PingDbContext : DbContext
{
    public PingDbContext(DbContextOptions<PingDbContext> options) : base(options) {}
}