using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data
{
    public class ApplicantDbContext : DbContext
    {
        public DbSet<Core.Applicant.Applicant> Applicants { get; set; }

        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options): base(options)
        {
        }
    }
}