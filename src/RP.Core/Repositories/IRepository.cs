using RP.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.Core.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
    }

    public interface IAlbumRepository : IRepository<Album> { }
    public interface IPhotoRepository : IRepository<Photo> { }
}
