using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Mortgage
{
    public interface IMortgageRepository
    {
        public Task<IEnumerable<Mortgage>> GetMortgages();
        public Task<Mortgage> GetMortgage(long id);
        public Task<long> CreateMortgage(Mortgage mortgage);
        public Task DeleteMortgage(long id);
        public bool MortgageExists(long id); 
    }
}