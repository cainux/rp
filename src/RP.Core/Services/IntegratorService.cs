using RP.Core.Entities;
using RP.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RP.Core.Services
{
    public class IntegratorService : IIntegratorService
    {
        private IAlbumRepository albumRepository;
        private IPhotoRepository photoRepository;

        public IntegratorService(IAlbumRepository albumRepository, IPhotoRepository photoRepository)
        {
            this.albumRepository = albumRepository;
            this.photoRepository = photoRepository;
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            var albumsDict = new Dictionary<int, Album>();

            var albumsGetter = albumRepository.GetAllAsync();
            var photosGetter = photoRepository.GetAllAsync();

            var sourceAlbums = await albumsGetter;

            foreach (var sourceAlbum in sourceAlbums)
            {
                albumsDict.Add(sourceAlbum.Id, sourceAlbum);
            }

            var sourcePhotos = await photosGetter;

            foreach (var sourcePhoto in sourcePhotos)
            {
                if (albumsDict.ContainsKey(sourcePhoto.AlbumId))
                {
                    var album = albumsDict[sourcePhoto.AlbumId];

                    if (album.Photos == null)
                    {
                        album.Photos = new List<Photo>();
                    }

                    album.Photos.Add(sourcePhoto);
                }
            }

            return albumsDict.Select(x => x.Value);
        }

        public async Task<IEnumerable<Album>> GetByUserIdAsync(int userId)
        {
            var albumsDict = new Dictionary<int, Album>();

            var albumsGetter = albumRepository.GetAllAsync();
            var photosGetter = photoRepository.GetAllAsync();

            var sourceAlbums = await albumsGetter;

            foreach (var sourceAlbum in sourceAlbums.Where(x => x.UserId == userId))
            {
                albumsDict.Add(sourceAlbum.Id, sourceAlbum);
            }

            var sourcePhotos = await photosGetter;

            foreach (var sourcePhoto in sourcePhotos)
            {
                if (albumsDict.ContainsKey(sourcePhoto.AlbumId))
                {
                    var album = albumsDict[sourcePhoto.AlbumId];

                    if (album.Photos == null)
                    {
                        album.Photos = new List<Photo>();
                    }

                    album.Photos.Add(sourcePhoto);
                }
            }

            return albumsDict.Select(x => x.Value);
        }
    }
}
