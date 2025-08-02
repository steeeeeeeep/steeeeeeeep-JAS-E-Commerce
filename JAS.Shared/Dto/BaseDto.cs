namespace JAS.Shared.Dto;

public class BaseDto
{
    public bool IsDeleted { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } 
    public string? ModifiedBy { get; set; }
    public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
}
