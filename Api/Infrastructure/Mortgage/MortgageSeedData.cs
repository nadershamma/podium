using System;
using System.Linq;
using Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Mortgage
{
    public static class MortgageSeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using var mortgageDbContext =
                new MortgageDbContext(serviceProvider.GetRequiredService<DbContextOptions<MortgageDbContext>>());
            if (mortgageDbContext.Mortgages.Any())
            {
                return;
            }

            mortgageDbContext.Mortgages.AddRange(
                new Core.Mortgage.Mortgage
                {
                    Lender = "Bank A",
                    Rate = 2,
                    Type = "Variable",
                    LoanToValue = 60
                },
                new Core.Mortgage.Mortgage
                {
                    Lender = "Bank B",
                    Rate = 3,
                    Type = "Fixed",
                    LoanToValue = 60
                },
                new Core.Mortgage.Mortgage
                {
                    Lender = "Bank C",
                    Rate = 4,
                    Type = "Variable",
                    LoanToValue = 90
                }
            );
            mortgageDbContext.SaveChanges();
        }
    }
}