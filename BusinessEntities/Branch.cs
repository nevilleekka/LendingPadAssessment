using BusinessEntities.Base;
using Common.Extensions;

namespace BusinessEntities;

public record Branch : IdObject
{
    public ICollection<User> Members { get; set; }
    public string Name { get; set; }
}
