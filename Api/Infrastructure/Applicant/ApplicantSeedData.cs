using System;
using System.Linq;
using Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Applicant
{
    public static class ApplicantSeedData
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            using var applicantDbContext =
                new ApplicantDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicantDbContext>>());
            if (applicantDbContext.Applicants.Any())
            {
                return;
            }
            applicantDbContext.Applicants.AddRange(
                new Core.Applicant.Applicant
                {
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    DateOfBirth = new DateTime(1970, 05, 7),
                    Email = "joebloggs@example.com"
                },
                new Core.Applicant.Applicant
                {
                    FirstName = "Henry",
                    LastName = "Tudor",
                    DateOfBirth = new DateTime(1969, 05, 7),
                    Email = "henrytudor@example.com"
                }
            );
            applicantDbContext.SaveChanges();
        }
    }
}