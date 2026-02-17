namespace AiDialSdk.Api.JsonConverters.Internal;

internal static class JsonSimpleTypes
{
    public static readonly HashSet<Type> Value =
    [
        typeof(string),
        typeof(bool),
        typeof(byte), 
        typeof(sbyte),
        typeof(short), 
        typeof(ushort),
        typeof(int), 
        typeof(uint),
        typeof(long), 
        typeof(ulong),
        typeof(float), 
        typeof(double), 
        typeof(decimal),
        typeof(DateTime), 
        typeof(Guid),
        typeof(TimeSpan),
        typeof(DateTimeOffset)
    ];
}