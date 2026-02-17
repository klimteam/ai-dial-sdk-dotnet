namespace AiDialSdk.Api.Extensions;

public interface ICombinable<T>
{
    T Combine(T other);
}