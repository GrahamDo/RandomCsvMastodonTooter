// ReSharper disable UnusedAutoPropertyAccessor.Global
// All properties must have public getters and setters in order for serialisation to work
namespace RandomCsvMastodonTooter;

internal class MastodonStatus
{
    public object Status { get; set; } = string.Empty;
}