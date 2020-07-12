using Api.Core.Mortgage;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class MortgageDbContext: DbContext
    {
        public DbSet<Mortgage> Mortgages { get; set; }

        public MortgageDbContext(DbContextOptions<MortgageDbContext> options): base(options)
        {
        }
    }
}