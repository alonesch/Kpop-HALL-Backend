using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace KpopHall.Application.Photocards.GetPhotocard;

public class GetPhotocardUseCase
{
    private readonly IPhotoCardsRepository _photoCardRepository;
    private readonly string _cdnBaseUrl;

    public GetPhotocardUseCase(IPhotoCardsRepository photoCardRepository, IConfiguration configuration)
    {
        _photoCardRepository = photoCardRepository;
        _cdnBaseUrl = configuration["Cdn:BaseUrl"] ?? string.Empty;
    }

    public async Task<GetPhotocardResponse> ExecuteAsync(Guid id)
    {
        var photocard = await _photoCardRepository.GetByIdAsync(id);
        if (photocard == null)
            throw new DomainException("Photocard not found.");

        return new GetPhotocardResponse
        {
            Id = photocard.Id,
            Version = photocard.Version,
            AlbumId = photocard.AlbumId,
            MemberId = photocard.MemberId,
            IsIrregular = photocard.IsIrregular,
            Store = photocard.DistributionContext?.Store,
            Region = photocard.DistributionContext?.Region,
            Event = photocard.DistributionContext?.Event,
            PrintQuantity = photocard.DistributionContext?.PrintQuantity,
            FrontsideImageUrl = photocard.FrontsideImagePath != null
                ? $"{_cdnBaseUrl}/{photocard.FrontsideImagePath}"
                : null,
            BacksideImageUrl = photocard.BacksideImagePath != null
                ? $"{_cdnBaseUrl}/{photocard.BacksideImagePath}"
                : null
        };
    }
}