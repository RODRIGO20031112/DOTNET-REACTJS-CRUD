using Microsoft.EntityFrameworkCore;

namespace rodrigo_server;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}