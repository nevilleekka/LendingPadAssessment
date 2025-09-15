using BusinessEntities.Base;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities;

public record Order : IdObject
{
    [Required]
    public string Customer { get; set; }
    
    public Guid ProductId { get; set; }
    public ICollection<Product> Products { get; set; }
}

