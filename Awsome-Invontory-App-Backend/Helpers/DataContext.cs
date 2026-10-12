using Awsome_Invontory_App_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Awsome_Invontory_App_Backend.Helpers;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }
}