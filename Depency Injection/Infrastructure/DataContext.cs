using Depency_Injection.Models;
using Microsoft.EntityFrameworkCore;

namespace Depency_Injection.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Product> Products{ get; set; }
    public DbSet<Category> Categories { get; set; }

}
