using Refit;
using RP.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RP.Infrastructure.External
{
    public interface IJsonPlaceHolderApi
    {
        [Get("/albums")]
        Task<IEnumerable<AlbumDto>> GetAlbums();

        [Get("/photos")]
        Task<IEnumerable<PhotoDto>> GetPhotos();
    }
}
