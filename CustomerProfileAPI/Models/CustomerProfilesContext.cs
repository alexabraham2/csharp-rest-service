using Microsoft.EntityFrameworkCore;

namespace CustomerProfileAPI.Models;

public class CustomerProfilesContext : DbContext
{
    public CustomerProfilesContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<CustomerProfile> CustomerProfiles { get; set; } = null!;
}
