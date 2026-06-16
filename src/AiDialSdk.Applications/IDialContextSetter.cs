namespace AiDialSdk.Applications;

internal interface IDialContextSetter<in TDialContext> where TDialContext : BaseContext
{
    void SetCompletionContext(TDialContext completionContext);
}