using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Applicant;

namespace Api.Core.Applicant
{
    public interface IApplicantService
    {
        public Task<IEnumerable<Applicant>> GetApplicants();
        public Task<Applicant> GetApplicant(long id);
        public Task<long> CreateApplicant(CreateApplicantDto createApplicantDto);
        public Task DeleteApplicant(long id);
        public Task<bool> ApplicantExists(long id);
    }
}