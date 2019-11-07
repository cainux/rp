using RP.Core.Entities;
using RP.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RP.Core.Services
{
    public class IntegratorService : IIntegratorService
    {
        private readonly IAlbumRepository albumRepository;
        private readonly IPhotoRepository photoRepository;

        public IntegratorService(IAlbumRepository albumRepository, IPhotoRepository photoRepository)
        {
            this.albumRepository = albumRepository;
            this.photoRepository = photoRepository;
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            var albumsDict = new Dictionary<int, Album>();

            var albumsGetter = albumRepository.GetAsync();
            var photosGetter = photoRepository.GetAsync();

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
                    albumsDict[sourcePhoto.AlbumId].Photos.Add(sourcePhoto);
                }
            }

            return albumsDict.Select(x => x.Value);
        }

        public async Task<IEnumerable<Album>> GetByUserIdAsync(int userId)
        {
            var albumsDict = new Dictionary<int, Album>();
            var albums = (await albumRepository.GetAsync(userId)).ToArray();

            foreach (var album in albums)
            {
                albumsDict.Add(album.Id, album);
            }

            var photoGetters = new Task<IEnumerable<Photo>>[albums.Length];

            for (int i = 0; i < albums.Length; i++)
            {
                photoGetters[i] = photoRepository.GetAsync(albums[i].Id);
            }

            foreach(var photos in await Task.WhenAll(photoGetters))
            {
                foreach (var photo in photos)
                {
                    albumsDict[photo.AlbumId].Photos.Add(photo);
                }
            }

            return albumsDict.Select(x => x.Value);
        }
    }
}
