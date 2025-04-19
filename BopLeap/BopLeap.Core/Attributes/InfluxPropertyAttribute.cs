namespace BopLeap.Core.Attributes
{
    public enum InfluxPropertyKind
    {
        Field,
        Tag,
        Timestamp
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class InfluxPropertyAttribute(InfluxPropertyKind kind) : Attribute
    {
        public InfluxPropertyKind Kind { get; } = kind;
    }
}
