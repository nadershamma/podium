using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Mortgage;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Applicant
{
    public class MortgageRepository : IMortgageRepository
    {
        private readonly MortgageDbContext _context;

        public MortgageRepository(MortgageDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mortgage>> GetMortgages()
        {
            return await _context.Mortgages.ToListAsync();
        }

        public async Task<Mortgage> GetMortgage(long id)
        {
            return await _context.Mortgages.FindAsync(id);
        }

        public async Task<long> CreateMortgage(Mortgage mortgage)
        {
            await _context.Mortgages.AddAsync((mortgage));
            await _context.SaveChangesAsync();
            return mortgage.Id;
        }

        public async Task DeleteMortgage(long id)
        {
            var mortgage = await _context.Mortgages.FindAsync(id);
            _context.Mortgages.Remove(mortgage);
            await _context.SaveChangesAsync();
        }

        public bool MortgageExists(long id)
        { 
            return _context.Mortgages.Any(e => e.Id == id);
        
        }
    }
}