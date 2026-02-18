public interface IDialContextAccessor<out TDialContext> where TDialContext : BaseContext
{
    TDialContext Context { get; }
}