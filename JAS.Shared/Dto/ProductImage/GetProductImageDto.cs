namespace JAS.Shared.Dto.ProductImage;
public class GetProductImageDto : BaseDto
{
    public long ProductImageId { get; set; }
    public long ProductId { get; set; }

    public string Url { get; set; }
}
