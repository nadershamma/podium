using Api.Core.Applicant;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicantDbContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }

        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options): base(options)
        {
        }
    }
}