// ReSharper disable UnusedAutoPropertyAccessor.Global
// All properties must have public getters and setters in order for serialisation to work

using Newtonsoft.Json;

namespace RandomCsvMastodonTooter;

internal class MastodonStatus
{
    [JsonProperty("status")]
    public object Status { get; set; } = string.Empty;
}