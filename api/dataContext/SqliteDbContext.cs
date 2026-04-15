using Microsoft.EntityFrameworkCore;

namespace contact_app.dataContext;

public class SqliteDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {

    }
}
