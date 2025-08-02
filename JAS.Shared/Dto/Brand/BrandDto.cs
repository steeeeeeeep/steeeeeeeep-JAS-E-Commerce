namespace JAS.Shared.Dto.Brand;

public class BrandDto : BaseDto
{
    public int BrandId { get; set; }
    public string BrandName { get; set; }

    public string Description { get; set; }
    public bool IsFeatured { set; get; }

}
