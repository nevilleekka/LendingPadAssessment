using System.ComponentModel.DataAnnotations;

namespace BusinessEntities.Base;

public record IdNameObject : IdObject
{
    [Required]
    public string Name { get; set; } 
}
