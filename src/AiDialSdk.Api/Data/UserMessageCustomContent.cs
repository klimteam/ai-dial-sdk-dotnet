using System.Text.Json.Serialization;

namespace AiDialSdk.Api.Data;

public class UserMessageCustomContent
{
    public UserMessageCustomContent(DialFormValue? formValue)
    {
        FormValue = formValue;
    }
    
    public DialFormValue? FormValue { get; }
    
    [JsonIgnore]
    public bool HasFormValues => FormValue != null && FormValue.Any();
    
    public TForm? GetFormValueFromCustomContent<TForm, TValue>()
        where TForm : Dictionary<string, TValue?>, new()
    {
        if (FormValue is null)
        {
            return null;
        }

        var result = new TForm();
        
        foreach (var formValue in FormValue)
        {
            result.Add(formValue.Key, (TValue?)formValue.Value);
        }

        return result;
    }
}