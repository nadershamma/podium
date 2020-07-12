using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Applicant;
using Api.Core.Mortgage;

namespace Api.Application.Mortgage
{
    public class MortgageService : IMortgageService
    {
        private readonly IMortgageRepository _mortgageMortgageRepository;
        private readonly IApplicantRepository _applicantRepository;

        public MortgageService(IMortgageRepository mortgageRepository, IApplicantRepository applicantRepository)
        {
            _mortgageMortgageRepository = mortgageRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<IEnumerable<Core.Mortgage.Mortgage>> GetMortgages()
        {
            return await _mortgageMortgageRepository.GetMortgages();
        }

        public async Task<Core.Mortgage.Mortgage> GetMortgage(long id)
        {
            return await _mortgageMortgageRepository.GetMortgage(id);
        }

        public async Task<IEnumerable<Core.Mortgage.Mortgage>> GetQualifiedMortgages(long applicantId, decimal propertyValue,
            decimal depositAmount)
        {
            
            if (_applicantRepository.ApplicantExists(applicantId))
            {
                DateTime today = DateTime.Now;
                var applicant = await _applicantRepository.GetApplicant(applicantId);
                TimeSpan age = today - applicant.DateOfBirth;
                if ((age.TotalDays / 365) >= 18)
                {
                    var mortgages = (List<Core.Mortgage.Mortgage>) await _mortgageMortgageRepository.GetMortgages();
                    decimal mortgageAmount = propertyValue - depositAmount;
                    int ltv = (int) ((mortgageAmount / propertyValue) * 100);
                    if (ltv <= 90)
                    {
                        return mortgages.Where(m => m.LoanToValue >= ltv).ToList();
                    }
                }
            }
            return new List<Core.Mortgage.Mortgage>();
        }

        public async Task<long> CreateMortgage(CreateMortgageDto createMortgageDto)
        {
            var mortgage = new Core.Mortgage.Mortgage
            {
                Lender = createMortgageDto.Lender,
                Rate = createMortgageDto.Rate,
                Type = createMortgageDto.Type,
                LoanToValue = createMortgageDto.LoanToValue
            };
            return await _mortgageMortgageRepository.CreateMortgage(mortgage);
        }

        public async Task DeleteMortgage(long id)
        {
            await _mortgageMortgageRepository.DeleteMortgage(id);
        }

        public bool MortgageExists(long id)
        {
            return _mortgageMortgageRepository.MortgageExists(id);
        }
    }
}