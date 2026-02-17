namespace AiDialSdk.Api.Data;

public class Layout
{
    public Layout(int height, int width)
    {
        Height = height;
        Width = width;
    }
    
    public int Height { get; }

    public int Width { get; }
}