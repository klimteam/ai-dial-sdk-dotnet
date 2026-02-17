namespace AiDialSdk.Api.OpenAi.Data;

public class Function
{
    public string Name { get; }
    
    public string Arguments { get; }
    
    public Function(string name, string arguments)
    {
        Name = name;
        Arguments = arguments;
    }
}