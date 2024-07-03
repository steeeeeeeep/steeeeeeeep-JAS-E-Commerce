namespace JASData.Models;

public class BaseEntity
{
    public bool IsDeleted { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
}
