using Microsoft.EntityFrameworkCore;
using BackEnd.Model;
namespace BackEnd.Data
{
    public class MarketDBContext : DbContext
    {
        public MarketDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Comment> comments { get; set; }

    }
}
