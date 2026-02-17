namespace AiDialSdk.Api.OpenAi.Data;

public enum FinishReason : byte
{
    Stop,
    Length,
    ContentFilter,
    ToolCalls,
    FunctionCalls
}