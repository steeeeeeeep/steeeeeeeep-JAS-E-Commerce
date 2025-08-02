using System.ComponentModel.DataAnnotations;

namespace JAS.Shared.Dto.Purchase;

public class PurchaseDto : BaseDto
{
    public long PurchaseId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
