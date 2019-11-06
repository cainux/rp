using RP.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.Core.Services
{
    public class IntegratorService : IIntegratorService
    {
        public Task<IEnumerable<Album>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Album>> GetByUserIdAsync(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
