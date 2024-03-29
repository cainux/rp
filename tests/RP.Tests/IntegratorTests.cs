using FluentAssertions;
using Moq;
using Moq.AutoMock;
using RP.Core.Entities;
using RP.Core.Repositories;
using RP.Core.Services;
using Xunit;

namespace RP.Tests
{
    public class IntegratorTests
    {
        private readonly AutoMocker mocker = new AutoMocker();
        private readonly IIntegratorService SUT;

        public IntegratorTests()
        {
            SUT = mocker.CreateInstance<IntegratorService>();
        }

        [Fact]
        public void Correctly_Integrates_Photos_Into_Albums()
        {
            // Arrange
            var mockAlbumRepo = mocker.GetMock<IAlbumRepository>();
            var mockPhotoRepo = mocker.GetMock<IPhotoRepository>();

            mockAlbumRepo.Setup(x => x.GetAsync(null)).ReturnsAsync(new[]
            {
                new Album { Id = 1, UserId = 1 },
                new Album { Id = 2, UserId = 1 },
                new Album { Id = 3, UserId = 2 },
                new Album { Id = 4, UserId = 3 }
            })
            .Verifiable();

            mockPhotoRepo.Setup(x => x.GetAsync(null)).ReturnsAsync(new[]
            {
                new Photo { Id = 1, AlbumId = 1 },
                new Photo { Id = 2, AlbumId = 1 },
                new Photo { Id = 3, AlbumId = 2 },
                new Photo { Id = 4, AlbumId = 2 },
                new Photo { Id = 5, AlbumId = 3 },
                new Photo { Id = 6, AlbumId = 3 },
                new Photo { Id = 7, AlbumId = 4 },
                new Photo { Id = 8, AlbumId = 4 }
            })
            .Verifiable();

            mocker.Use(mockAlbumRepo.Object);
            mocker.Use(mockPhotoRepo.Object);

            // Act
            var actual = SUT.GetAllAsync().Result;

            // Assert
            mockAlbumRepo.VerifyAll();
            mockPhotoRepo.VerifyAll();

            actual.Should().BeEquivalentTo(new[]
            {
                new
                {
                    Id = 1, UserId = 1,
                    Photos = new[]
                    {
                        new { Id = 1, AlbumId = 1 },
                        new { Id = 2, AlbumId = 1 }
                    }
                },
                new
                {
                    Id = 2, UserId = 1,
                    Photos = new[]
                    {
                        new { Id = 3, AlbumId = 2 },
                        new { Id = 4, AlbumId = 2 }
                    }
                },
                new
                {
                    Id = 3, UserId = 2,
                    Photos = new[]
                    {
                        new { Id = 5, AlbumId = 3 },
                        new { Id = 6, AlbumId = 3 }
                    }
                },
                new
                {
                    Id = 4, UserId = 3,
                    Photos = new[]
                    {
                        new { Id = 7, AlbumId = 4 },
                        new { Id = 8, AlbumId = 4 }
                    }
                }
            });
        }

        [Fact]
        public void Correctly_Filters_By_UserId()
        {
            // Arrange
            var userId = 3;
            var mockAlbumRepo = mocker.GetMock<IAlbumRepository>();
            var mockPhotoRepo = mocker.GetMock<IPhotoRepository>();

            mockAlbumRepo.Setup(x => x.GetAsync(userId)).ReturnsAsync(new[]
            {
                new Album { Id = 4, UserId = 3 },
                new Album { Id = 5, UserId = 3 }
            })
            .Verifiable();

            mockPhotoRepo.Setup(x => x.GetAsync(4)).ReturnsAsync(new[]
            {
                new Photo { Id = 7, AlbumId = 4 },
                new Photo { Id = 8, AlbumId = 4 }
            })
            .Verifiable();

            mockPhotoRepo.Setup(x => x.GetAsync(5)).ReturnsAsync(new[]
            {
                new Photo { Id = 9, AlbumId = 5 }
            })
            .Verifiable();

            mocker.Use(mockAlbumRepo.Object);
            mocker.Use(mockPhotoRepo.Object);

            // Act
            var actual = SUT.GetByUserIdAsync(userId).Result;

            // Assert
            mockAlbumRepo.VerifyAll();
            mockPhotoRepo.VerifyAll();

            actual.Should().BeEquivalentTo(new[]
            {
                new
                {
                    Id = 4, UserId = 3,
                    Photos = new[]
                    {
                        new { Id = 7, AlbumId = 4 },
                        new { Id = 8, AlbumId = 4 }
                    }
                },
                new
                {
                    Id = 5, UserId = 3,
                    Photos = new[]
                    {
                        new { Id = 9, AlbumId = 5 }
                    }
                }
            });
        }
    }
}
