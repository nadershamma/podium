using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Applicant
{
    public interface IApplicantRepository
    {
        public Task<IEnumerable<Applicant>> GetApplicants();
        public Task<Applicant> GetApplicant(long id);
        public Task<long> CreateApplicant(Applicant applicant);
        public Task DeleteApplicant(long id);
        public bool ApplicantExists(long id);
    }
}