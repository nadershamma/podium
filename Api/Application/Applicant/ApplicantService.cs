using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Applicant;

namespace Api.Application.Applicant
{
    public class ApplicantService: IApplicantService
    {
        private readonly IApplicantRepository _repository;

        public ApplicantService(IApplicantRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Core.Applicant.Applicant>> GetApplicants()
        {
            return await _repository.GetApplicants();
        }

        public async Task<Core.Applicant.Applicant> GetApplicant(long id)
        {
            return await _repository.GetApplicant(id);
        }

        public async Task<long> CreateApplicant(CreateApplicantDto createApplicantDto)
        {
            var applicant = new Core.Applicant.Applicant
            {
                FirstName = createApplicantDto.FirstName,
                LastName = createApplicantDto.LastName,
                DateOfBirth = createApplicantDto.DateOfBirth,
                Email = createApplicantDto.Email,
            };
            return await _repository.CreateApplicant(applicant);
        }

        public async Task DeleteApplicant(long id)
        {
            await _repository.DeleteApplicant(id);
        }
        
        public async Task<bool> ApplicantExists(long id)
        {
            return await _repository.ApplicantExists(id);
        }
    }
}