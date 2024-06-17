using System.Text.Json.Serialization;

namespace Application.Adapters.Internals;

public class ResponseInternalAdapter
{    
    [JsonIgnore]
    public int StatusCode { get; set; }
    public List<FieldErrorInternalAdapter>? Errors { get; set; }
    public dynamic? Data { set; get; }
}
