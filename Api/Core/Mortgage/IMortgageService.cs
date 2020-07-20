using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Mortgage;

namespace Api.Core.Mortgage
{
    public interface IMortgageService
    {
        public Task<IEnumerable<Mortgage>> GetMortgages();
        public Task<Mortgage> GetMortgage(long id);
        public Task<IEnumerable<Mortgage>> GetQualifiedMortgages(long applicantId, decimal propertyValue, decimal depositAmount);
        public Task<long> CreateMortgage(CreateMortgageDto createMortgageDto);
        public Task DeleteMortgage(long id);
        public Task<bool> MortgageExists(long id);
    }
}