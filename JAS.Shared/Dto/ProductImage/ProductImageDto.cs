namespace JAS.Shared.Dto.ProductImage;
public class ProductImageDto : BaseDto
{
    public long ProductImageId { get; set; }
    public long ProductId { get; set; }

    public string Url { get; set; }
}
