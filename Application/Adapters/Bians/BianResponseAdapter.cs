using Application.Adapters.Internals;
using System.Text.Json.Serialization;

namespace Application.Adapters.Bians;

public class BianResponseAdapter
{    
    [JsonIgnore]
    public int statusCode {  get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<BianErrorInternalAdapter>? errors { get; set; }
}
