using BusinessEntities.Base;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities;

public record Product : IdNameObject
{

    public string Description { get; set; }

}

