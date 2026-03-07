using KpopHall.Application.Interfaces;
using KpopHall.Domain.Exceptions;
namespace KpopHall.Application.Photocards.GetPhotocard;

public class GetPhotocardUseCase
{
    private readonly IPhotoCardsRepository _photoCardRepository;
    public GetPhotocardUseCase(IPhotoCardsRepository photoCardRepository)
    {
        _photoCardRepository = photoCardRepository;
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
            PrintQuantity = photocard.DistributionContext?.PrintQuantity
        };
    }
}