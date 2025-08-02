namespace JAS.Shared.Dto.ProductImage;

public class AddOrUpdateProductImage : BaseDto
{
    public long ProductImageId { get; set; }
    public long ProductId { get; set; }

    public string Url { get; set; }
}
