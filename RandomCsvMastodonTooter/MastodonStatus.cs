// ReSharper disable UnusedAutoPropertyAccessor.Global
// All properties must have public getters and setters in order for serialisation to work

using System.Text.Json.Serialization;

namespace RandomCsvMastodonTooter;

internal class MastodonStatus
{
    [JsonPropertyName("status")]
    public object Status { get; set; } = string.Empty;
}