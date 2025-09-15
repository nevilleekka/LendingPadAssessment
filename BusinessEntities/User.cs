using BusinessEntities.Base;
using Common.Extensions;

namespace BusinessEntities;

public record User : IdObject
{

    public IEnumerable<string> Tags { get; set; }= new List<string>();
    public int Age { get; set; }
    public string Email { get; set; }//throw new ArgumentNullException("Name was not provided.");
    public decimal? MonthlySalary { get; set; }
    public string Name { get; set; }//throw new ArgumentNullException("Name was not provided.");
    public UserTypes Type { get; set; } = UserTypes.Employee;


}
