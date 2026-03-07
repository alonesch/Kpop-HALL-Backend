namespace KpopHall.Application.Photocards.CreatePhotocard;

public class CreatePhotocardRequest
{
    public Guid MemberId { get; set; }
    public string Version { get; set; } = null!;

    public DistributionContextRequest? DistributionContext { get; set; }
}

public class DistributionContextRequest
{
    public string Store { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Event { get; set; } = null!;
    public int PrintQuantity { get; set; }
}