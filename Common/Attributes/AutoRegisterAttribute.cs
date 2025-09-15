namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AutoRegisterAttribute : Attribute
{
    public AutoRegisterTypes Type { get; set; } = AutoRegisterTypes.Scope;

    public AutoRegisterAttribute()
    {
    }

    public AutoRegisterAttribute(AutoRegisterTypes type)
    {
        Type = type;
    }


}
