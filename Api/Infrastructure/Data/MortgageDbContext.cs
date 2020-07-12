using Api.Core.Mortgage;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data
{
    public class MortgageDbContext: DbContext
    {
        public DbSet<Core.Mortgage.Mortgage> Mortgages { get; set; }

        public MortgageDbContext(DbContextOptions<MortgageDbContext> options): base(options)
        {
        }
    }
}