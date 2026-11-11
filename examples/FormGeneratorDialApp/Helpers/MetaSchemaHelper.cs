namespace FormGeneratorDialApp.Helpers;

public static class MetaSchemaHelper
{
    private const string FileName = "metaSchema.json";
    
    public static Task<string> ReadMetaSchemaAsync(CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(AppContext.BaseDirectory, FileName);
        return File.ReadAllTextAsync(path, cancellationToken);
    }
}
