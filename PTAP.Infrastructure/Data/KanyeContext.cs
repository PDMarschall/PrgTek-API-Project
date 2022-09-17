using Microsoft.EntityFrameworkCore;
using PTAP.Core.Models;

namespace PTAP.Infrastructure.Data
{
    public class KanyeContext : DbContext
    {
        public KanyeContext(DbContextOptions<KanyeContext> options)
            : base(options)
        {
        }

        public DbSet<KanyeQuote> Quote { get; set; }
    }
}
