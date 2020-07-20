using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Core.Applicant;
using Api.Core.Mortgage;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Api.Application.Mortgage
{
    public class MortgageService : IMortgageService
    {
        private readonly IMortgageRepository _mortgageRepository;
        private readonly IApplicantRepository _applicantRepository;

        public MortgageService(IMortgageRepository mortgageRepository, IApplicantRepository applicantRepository)
        {
            _mortgageRepository = mortgageRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<IEnumerable<Core.Mortgage.Mortgage>> GetMortgages()
        {
            return await _mortgageRepository.GetMortgages();
        }

        public async Task<Core.Mortgage.Mortgage> GetMortgage(long id)
        {
            return await _mortgageRepository.GetMortgage(id);
        }

        public async Task<IEnumerable<Core.Mortgage.Mortgage>> GetQualifiedMortgages(long applicantId,
            decimal propertyValue,
            decimal depositAmount)
        {
            if (!await _applicantRepository.ApplicantExists(applicantId))
            {
                return null;
            }
            
            var applicant = await _applicantRepository.GetApplicant(applicantId);
            DateTime today = DateTime.Now;
            TimeSpan age = today - applicant.DateOfBirth;
            if ((age.TotalDays / 365) >= 18)
            {
                decimal mortgageAmount = propertyValue - depositAmount;
                int ltv = (int) ((mortgageAmount / propertyValue) * 100);
                if (ltv <= 90)
                {
                    return await _mortgageRepository.GetMortgages(m =>
                        m.LoanToValue >= ltv);
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
            return await _mortgageRepository.CreateMortgage(mortgage);
        }

        public async Task DeleteMortgage(long id)
        {
            await _mortgageRepository.DeleteMortgage(id);
        }

        public async Task<bool> MortgageExists(long id)
        {
            return await _mortgageRepository.MortgageExists(id);
        }
    }
}