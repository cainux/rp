using RP.Core.Entities;
using RP.Core.Repositories;
using RP.Infrastructure.External;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RP.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private IJsonPlaceHolderApi jsonPlaceHolderApi;

        public AlbumRepository(IJsonPlaceHolderApi jsonPlaceHolderApi)
        {
            this.jsonPlaceHolderApi = jsonPlaceHolderApi;
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            var data = await jsonPlaceHolderApi.GetAlbums();

            return data.Select(x => new Album
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title
            });
        }
    }
}
