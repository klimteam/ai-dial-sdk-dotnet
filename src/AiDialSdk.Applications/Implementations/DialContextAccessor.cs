namespace Implementations;

public class DialContextAccessor<TDialContext> : IDialContextAccessor<TDialContext>, IDialContextSetter<TDialContext>
    where TDialContext : BaseContext
{
    private TDialContext? _completionContext;
    
    public TDialContext Context => 
        _completionContext ?? throw new NullReferenceException("Completion context is not set");

    public void SetCompletionContext(TDialContext completionContext) =>
        _completionContext = completionContext;
}