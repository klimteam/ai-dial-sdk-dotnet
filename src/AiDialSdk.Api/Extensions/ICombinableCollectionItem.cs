namespace AiDialSdk.Api.Extensions;

public interface ICombinableCollectionItem<T> : ICombinable<T>
{
    int Index { get; }
}