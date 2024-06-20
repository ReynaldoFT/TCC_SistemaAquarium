using Microsoft.EntityFrameworkCore;

namespace SiteAquario.Data
{
    public class SiteAquarioContext : DbContext
    {
        public SiteAquarioContext (DbContextOptions<SiteAquarioContext> options)
            : base(options)
        {
        }

        public DbSet<SiteAquario.Models.ValoresSensor> ValoresSensor { get; set; } = default!;
    }
}
