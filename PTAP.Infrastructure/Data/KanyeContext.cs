using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTAP.Core.Models;

namespace PTAP.Infrastructure.Data
{
    public class KanyeContext : DbContext
    {
        public KanyeContext (DbContextOptions<KanyeContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quote { get; set; }
        public DbSet<KanyeImage> Image { get; set; }
    }
}
