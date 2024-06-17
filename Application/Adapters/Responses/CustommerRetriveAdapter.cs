using Application.Adapters.Bians;
using System.Text.Json.Serialization;

namespace Application.Adapters.Responses;

public class CustommerRetriveAdapter : BianResponseAdapter
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? RetrivePersonalInformationResponse { get; set; }
}
