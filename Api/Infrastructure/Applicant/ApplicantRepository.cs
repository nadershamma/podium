using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Applicant;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Applicant
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicantDbContext _context;

        public ApplicantRepository(ApplicantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Core.Applicant.Applicant>> GetApplicants()
        {
            return await _context.Applicants.ToListAsync();
        }

        public async Task<Core.Applicant.Applicant> GetApplicant(long id)
        {
            var applicant = await _context.Applicants.FindAsync(id);
            return applicant;
        }

        public async Task<long> CreateApplicant(Core.Applicant.Applicant applicant)
        {
            await _context.Applicants.AddAsync(applicant);
            await _context.SaveChangesAsync();
            return applicant.Id;
        }

        public async Task DeleteApplicant(long id)
        {
            var applicant = await _context.Applicants.FindAsync(id);
            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();
        }
        
        public bool ApplicantExists(long id)
        {
            return _context.Applicants.Any(e => e.Id == id);
        }
    }
}