using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessEntities.Base;

public abstract record IdObject
{
    [Key]
    public Guid Identifier { get; set; }

    [NotMapped]
    public string Id {  get; set; }
    
   
}
