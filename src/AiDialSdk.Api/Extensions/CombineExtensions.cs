namespace AiDialSdk.Api.Extensions;

public static class CombineExtensions
{
    public static IReadOnlyDictionary<TKey, TValue>? Combine<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue>? source, 
        IReadOnlyDictionary<TKey, TValue>? other) 
        where TKey : notnull
    {
        var result = source?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        
        if (other is null)
        {
            return result;
        }
        
        if (result is null)
        {
            return other;
        }

        foreach (var keyValuePair in other)
        {
            result[keyValuePair.Key] = keyValuePair.Value;
        }

        return result;
    }

    public static IReadOnlyList<T>? Combine<T>(this IReadOnlyCollection<T>? source, IReadOnlyCollection<T>? other)
    {
        var result = source?.ToHashSet();

        if (other is null)
        {
            return result?.ToArray();
        }
        
        if (result is null)
        {
            return other.ToList();
        }

        foreach (var item in other)
        {
            result.Add(item);
        }

        return result.ToArray();
    }

    public static IReadOnlyList<T>? Combine<T>(this IReadOnlyList<T>? source, IReadOnlyList<T>? other)
        where T : ICombinableCollectionItem<T>
    {
        var result = source?.ToList();

        if (other is null)
        {
            return result;
        }
        
        if (result is null)
        {
            return other;
        }
        
        foreach (var item in other)
        {
            if (item.Index == result.Count)
            {
                result.Add(item);
            }
            else if (item.Index < result.Count)
            {
                result[item.Index] = result[item.Index].Combine(item);
            }
            else
            {
                throw new Exception("Collection item indexes must be sequential");
            }
        }

        result.VerifyIndexes();
        return result;
    }
    
    private static void VerifyIndexes<T>(this IReadOnlyList<T> source)
        where T : ICombinableCollectionItem<T>
    {
        if (source.Where((t, i) => t.Index != i).Any())
        {
            throw new Exception("Collection item indexes must be sequential");
        }
    }
}