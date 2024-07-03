using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JASData.Models;

public class ProductCategory : BaseEntity
{
    [Required,Key]
    public int ProductCategoryId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    public string? Description { get; set; }
    public int Quantity { get; set; }
    public bool Featured { get; set; }
}
