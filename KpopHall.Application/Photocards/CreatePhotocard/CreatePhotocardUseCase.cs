using KpopHall.Domain.Entities;
using KpopHall.Domain.Exceptions;
using KpopHall.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace KpopHall.Application.Photocards.CreatePhotocard
{
    public class CreatePhotocardUseCase
    {
        private readonly IPhotoCardRepository _photoCardRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;

        public CreatePhotocardUseCase(IPhotoCardRepository photoCardRepository, IAlbumRepository albumRepository, IArtistRepository artistRepository)
        {
            _photoCardRepository = photoCardRepository;
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
        }
        public async Task<CreatePhotocardResponse> ExecuteAsync(int artistId, int albumId, CreatePhotocardRequest request)
        {
            var artist = await _artistRepository.GetByIdAsync(artistId);
            if (artist == null)
                throw new DomainException("Artist not found.");

            var album = await _albumRepository.GetByIdAsync(albumId);
            if (album == null || album.ArtistId != artistId)
                throw new DomainException("Album not found.");

            var exists = await _photoCardRepository.ExistsByNameAndAlbumIdAsync(request.Name, albumId);
            if (exists)
                throw new DomainException("A photocard with the same name already exists in this album.");

            Photocard photocard;

            if (request.DistributionContext == null)
            {
                photocard = new Photocard(albumId, request.Name);
            }
            else
            {
                var context = new DistributionContext(
                    request.DistributionContext.Store,
                    request.DistributionContext.Region,
                    request.DistributionContext.Event,
                    request.DistributionContext.PrintQuantity
             );

                photocard = new Photocard(albumId, request.Name, context);
            }
                await _photoCardRepository.AddAsync(photocard);

                return new CreatePhotocardResponse
                {
                    Id = photocard.Id,
                    Name = photocard.Name,
                    AlbumId = photocard.AlbumId,
                    IsIrregular = photocard.IsIrregular
                };
            
        }
    }
}
