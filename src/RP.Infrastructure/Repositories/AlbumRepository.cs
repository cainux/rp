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
        private readonly IJsonPlaceHolderApi jsonPlaceHolderApi;

        public AlbumRepository(IJsonPlaceHolderApi jsonPlaceHolderApi)
        {
            this.jsonPlaceHolderApi = jsonPlaceHolderApi;
        }

        public async Task<IEnumerable<Album>> GetAsync(int? userId = null)
        {
            var data = await jsonPlaceHolderApi.GetAlbumsAsync(userId);

            return data.Select(x => new Album
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title
            });
        }
    }
}
