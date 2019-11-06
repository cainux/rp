using RP.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.Core.Services
{
    public interface IIntegratorService
    {
        Task<IEnumerable<Album>> GetAllAsync();
        Task<IEnumerable<Album>> GetByUserIdAsync(int userId);
    }
}
