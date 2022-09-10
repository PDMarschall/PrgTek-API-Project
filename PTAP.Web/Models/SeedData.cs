using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTAP.Core.Models;
using PTAP.Infrastructure.Data;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KanyeContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<KanyeContext>>()))
            {
                // Look for any movies.
                if (context.Quote.Any())
                {
                    return;   // DB has been seeded
                }

                context.Quote.AddRange(
                    new Quote
                    {
                        QuoteText = "Artists are founders"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}