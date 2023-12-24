

using Microsoft.EntityFrameworkCore;
using Raythos_Aerospace.Models;

namespace Raythos_Aerospace.Data
{
    public class AplicationDbContext :DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<Catagory> Catagorys { get; set; }
    }
}
