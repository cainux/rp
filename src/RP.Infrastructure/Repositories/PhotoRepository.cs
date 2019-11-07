using RP.Core.Entities;
using RP.Core.Repositories;
using RP.Infrastructure.External;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RP.Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private IJsonPlaceHolderApi jsonPlaceHolderApi;

        public PhotoRepository(IJsonPlaceHolderApi jsonPlaceHolderApi)
        {
            this.jsonPlaceHolderApi = jsonPlaceHolderApi;
        }

        public async Task<IEnumerable<Photo>> GetAsync(int? albumId = null)
        {
            var data = await jsonPlaceHolderApi.GetPhotosAsync(albumId);

            return data.Select(x => new Photo
            {
                Id = x.Id,
                AlbumId = x.AlbumId,
                Title = x.Title,
                Url = x.Url,
                ThumbnailUrl = x.ThumbnailUrl
            });
        }
    }
}
