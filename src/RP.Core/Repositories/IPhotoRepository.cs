using RP.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.Core.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAsync(int? albumId = null);
    }
}
